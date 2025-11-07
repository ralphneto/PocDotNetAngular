using WebApiTest.Models;

namespace WebApiTest.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> GetByIdAsync(int id);

        Task AddAsync(Cliente cliente);

        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);

    }
}
