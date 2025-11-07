using Microsoft.EntityFrameworkCore;
using WebApiTest.Models;

namespace WebApiTest.Data
{
    public class ClienteContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options) { }
    }
}
