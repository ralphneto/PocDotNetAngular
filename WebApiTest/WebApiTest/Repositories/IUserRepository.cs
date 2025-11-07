using Microsoft.AspNetCore.Identity;
using WebApiTest.Models;

namespace WebApiTest.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(User newUser);
        Task AddLoginAsync(User user, UserLoginInfo info);
    }
}
