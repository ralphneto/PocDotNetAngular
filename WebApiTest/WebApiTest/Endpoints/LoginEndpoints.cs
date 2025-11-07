using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiTest.Models;
using WebApiTest.Services;

namespace WebApiTest.Endpoints
{
    public static class LoginEndpoints
    {
        public static WebApplication AddLoginEndpoints(this WebApplication app)
        {
            app.MapPost("/api/account/login/google", ([FromQuery] string returnUrl, LinkGenerator linkGenerator,
                SignInManager<User> signInManager, HttpContext context) =>
            {
                var properties = signInManager.ConfigureExternalAuthenticationProperties("Google",
                    linkGenerator.GetPathByName(context, "GoogleLoginCallback") + $"?returnUrl={returnUrl}");

                return Results.Challenge(properties, ["Google"]);

            });

            app.MapGet("/api/account/login/google", ([FromQuery] string returnUrl, LinkGenerator linkGenerator,
                SignInManager<User> signInManager, HttpContext context) =>
            {
                var properties = signInManager.ConfigureExternalAuthenticationProperties("Google",
                    linkGenerator.GetPathByName(context, "GoogleLoginCallback") + $"?returnUrl={returnUrl}");

                return Results.Challenge(properties, ["Google"]);

            });

            app.MapGet("/api/account/login/google/callback", async ([FromQuery] string returnUrl, 
                HttpContext context, IUserService userService) =>
            {
                var result = await context.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
                if (!result.Succeeded || result.Principal == null)
                {
                    return Results.Unauthorized();
                }

                await userService.LoginWithGoogleAsync(result.Principal);

                return Results.Redirect(returnUrl);

            }).WithName("GoogleLoginCallback");

            app.MapGet("/api/account/logout", [Authorize] async (
                ClaimsPrincipal user,
                IUserService authService) =>
            {
                var email = user.FindFirstValue(ClaimTypes.Email);

                if (string.IsNullOrEmpty(email))
                    return Results.BadRequest(new { message = "Invalid user context" });

                await authService.LogoutAsync(email);

                return Results.Ok(new { message = "Logout successful" });
            })
            .WithName("Logout")
            .WithTags("Authentication")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization();

            return app;
        }
    }
}
