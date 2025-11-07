using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Models;

namespace WebApiTest.Data
{
    public class UserContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        private DbSet<User> users;

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get => users; set => users = value; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Property(u => u.FirstName).HasMaxLength(256);

            builder.Entity<User>()
                .Property(u => u.LastName).HasMaxLength(256);
        }
    }
}

