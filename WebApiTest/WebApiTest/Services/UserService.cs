using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;
using WebApiTest.Exceptions;
using WebApiTest.Models;
using WebApiTest.Processors;
using WebApiTest.Repositories;

namespace WebApiTest.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IAuthTokenProcessor _authTokenProcessor;
        private readonly UserManager<User> _userManager;
       public UserService(IAuthTokenProcessor authTokenProcessor, UserManager<User> userManager,
       IUserRepository userRepository)
        {
            _authTokenProcessor = authTokenProcessor;
            _userManager = userManager;
            _repository = userRepository;
        }
        public async Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ExternalLoginProviderException("Google", "ClaimsPrincipal is null");
            }
            var email = claimsPrincipal?.FindFirstValue(ClaimTypes.Email);

            User user;
            try
            {
               user = await _userManager.FindByEmailAsync(email);
            } catch (Exception ex)
            {
                user = null;
            }
            

            if (user == null)
            {
                var newUser = new User
                {
                    Email = email,
                    UserName = email,
                    FirstName = claimsPrincipal?.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
                    LastName = claimsPrincipal?.FindFirstValue(ClaimTypes.Surname) ?? string.Empty,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(newUser);

                if (!result.Succeeded)
                {
                    throw new ExternalLoginProviderException("Google",
                        $"Unable to create user: {string.Join(", ",
                            result.Errors.Select(x => x.Description))}");
                }



                user = newUser;

            }

            var info = new UserLoginInfo("Google",
            claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty,
            "Google");

            var loginResult = await _userManager.AddLoginAsync(user, info);

            if (!loginResult.Succeeded)
            {
                throw new ExternalLoginProviderException("Google",
                    "Unable to login user: " + string.Join(", ", loginResult.Errors.Select(x => x.Description)));
            }

            var (jwtToken, expirationDateInUtc) = _authTokenProcessor.GenerateJwtToken(user);
            var refreshTokenValue = _authTokenProcessor.GenerateRefreshToken();

            var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(7);

            user.RefreshToken = refreshTokenValue;
            user.RefreshTokenExpiresAtUtc = refreshTokenExpirationDateInUtc;

            await _userManager.UpdateAsync(user);

            _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
            _authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);

        }

        public async Task LogoutAsync(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
                throw new ArgumentException("User email is required.", nameof(userEmail));

            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                return; // Usuário não existe mais ou já foi deslogado.

            // Obtém logins externos (ex: Google)
            var logins = await _userManager.GetLoginsAsync(user);

            foreach (var login in logins)
            {
                if (login.LoginProvider == "Google")
                {
                    // Remove o vínculo com o login externo
                    var removeResult = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    if (!removeResult.Succeeded)
                    {
                        throw new Exception($"Failed to remove external login ({login.LoginProvider}): " +
                                            string.Join(", ", removeResult.Errors.Select(e => e.Description)));
                    }
                }
            }

            // Invalida o refresh token salvo no banco
            user.RefreshToken = null;
            user.RefreshTokenExpiresAtUtc = null;
            await _userManager.UpdateAsync(user);

            // Remove cookies de autenticação
            _authTokenProcessor.RemoveAuthCookie("ACCESS_TOKEN");
            _authTokenProcessor.RemoveAuthCookie("REFRESH_TOKEN");

            // Se estiver usando Identity + SignInManager
            // await _signInManager.SignOutAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _repository.GetUserByEmailAsync(email);
        }


    }
}
