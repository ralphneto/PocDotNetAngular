using System.Security.Claims;
using WebApiTest.Models;

namespace WebApiTest.Services
{
    public interface IUserService
    {
        Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal);
        Task<User> GetUserByEmailAsync(string email);

        Task LogoutAsync(string email);
    }
}
