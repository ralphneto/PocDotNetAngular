using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Models;
using WebApiTest.Services;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            return Ok(await _clienteService.GetAllAsync()); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente is null) 
                return NotFound();
            
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Cliente cliente)
        {
            await _clienteService.AddAsync(cliente);
            return CreatedAtAction(nameof(Get), new {id = cliente.Id});
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            await _clienteService.UpdateAsync(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _clienteService.DeleteAsync(id);
            return NoContent();
        }
    }
}
