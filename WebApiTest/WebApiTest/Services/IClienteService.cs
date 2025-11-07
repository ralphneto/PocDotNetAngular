using System.Threading.Tasks;
using WebApiTest.Models;

namespace WebApiTest.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> GetByIdAsync(int id);

        Task AddAsync(Cliente cliente);

        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);
    }
}
