using WebApiTest.Models;

namespace WebApiTest.Processors
{
    public interface IAuthTokenProcessor
    {
        (string jwtToken, DateTime expiresAtUtc) GenerateJwtToken(User user);
        string GenerateRefreshToken();
        void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token, DateTime expiration);
        void RemoveAuthCookie(string cookieName);
    }
}
