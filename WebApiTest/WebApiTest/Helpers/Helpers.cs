using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace WebApiTest.Helpers
{
    internal class Helpers
    {
        internal class BasicAuthenticationHandler : Microsoft.AspNetCore.Authentication.AuthenticationHandler<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions>
        {
            public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
            {
            }

            public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
            {
            }

            protected override Task<AuthenticateResult> HandleAuthenticateAsync()
            {
                string username = null;
                try
                {
                    var authHeader = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                    var credentials = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                    username = credentials.FirstOrDefault();
                    var password = credentials.LastOrDefault();
                    if (username != "senior" || password != "password123")
                    {
                        return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));
                    }
                } catch {
                    return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
                }
                var claims = new[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, username) };
                var identity = new System.Security.Claims.ClaimsIdentity(claims, Scheme.Name);
                var principal = new System.Security.Claims.ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

        }
    }
}