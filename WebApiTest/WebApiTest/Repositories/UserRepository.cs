using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Data;
using WebApiTest.Models;

namespace WebApiTest.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context) => this._context = context;


        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task AddUserAsync(User newUser)
        {
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        public Task AddLoginAsync(User user, UserLoginInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
