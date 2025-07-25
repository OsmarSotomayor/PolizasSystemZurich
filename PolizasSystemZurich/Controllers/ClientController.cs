using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PolizasSystemZurich.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        /// <param name="clientDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientCreateDto clientDto)
        {
            await _clientService.AddAsync(clientDto);
            return CreatedAtAction(nameof(GetById), new { identificationNumber = clientDto.IdentificationNumber }, clientDto);
        }

        /// <summary>
        /// Actualiza cliente
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="clientDto"></param>
        /// <returns></returns>
        [HttpPut("{identificador}")]
        public async Task<IActionResult> Update(string identificador, [FromBody] ClientUpdateDto clientDto)
        {
            await _clientService.UpdateAsync(identificador, clientDto);
            return NoContent();
        }

        /// <summary>
        /// Elimina cliente
        /// </summary>
        /// <param name="identificador"></param>
        /// <returns></returns>
        [HttpDelete("{identificador}")]
        public async Task<IActionResult> Delete(string identificador)
        {
            await _clientService.DeleteAsync(identificador);
            return NoContent();
        }

        /// <summary>
        /// Consulta cliente por Id
        /// </summary>
        /// <param name="identificador"></param>
        /// <returns></returns>
        [HttpGet("{identificador}")]
        public async Task<ActionResult<ClientDto>> GetById(string identificador)
        {
            var client = await _clientService.GetByIdAsync(identificador);
            return Ok(client);
        }
        /// <summary>
        /// Filtra clientes
        /// </summary>
        /// <param name="name"> nombre del cliente</param>
        /// <param name="email"> email del clienye</param>
        /// <param name="identificador"> identificador unico </param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ClientDto>>> Filter(
            [FromQuery] string? name,
            [FromQuery] string? email,
            [FromQuery] int identificador)
        {
            var clients = await _clientService.FilterAsync(name, email, identificador);
            return Ok(clients);
        }

        /// <summary>
        /// Consulta todos los clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(clients);
        }
    }
}
