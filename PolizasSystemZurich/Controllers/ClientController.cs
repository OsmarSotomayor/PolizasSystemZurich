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
        public async Task<IActionResult> Create([FromBody] ClientCreateDto clientDto) //admin
        {
            int key = await _clientService.AddAsync(clientDto);
            return CreatedAtAction(nameof(GetById), new { identificador = key }, clientDto);
        }

        /// <summary>
        /// Actualiza cliente
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="clientDto"></param>
        /// <returns></returns>
        [HttpPut("{identificador}")]
        public async Task<IActionResult> Update(int identificador, [FromBody] ClientUpdateDto clientDto) //admin
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
        public async Task<IActionResult> Delete(int identificador) //admin
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
        public async Task<ActionResult<ClientDto>> GetById(int identificador) //admin
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
        public async Task<ActionResult<IEnumerable<ClientDto>>> Filter( //admin
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
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll() //cliente
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(clients);
        }

        /// <summary>
        /// Actualiza información específica del cliente (dirección y teléfono)
        /// </summary>
        /// <param name="identificationNumber">Número de identificación del cliente (10 dígitos)</param>
        /// <param name="clientDto">Datos a actualizar</param>
        /// <response code="204">Actualización exitosa</response>
        /// <response code="404">Cliente no encontrado</response>
        [HttpPatch("{identificationNumber:int}/actualizar-datos")] //cliente
        public async Task<IActionResult> UpdateClientData(
            [FromRoute] int identificationNumber,
            [FromBody] ClientUpdateDataDto clientDto)
        {
            try
            {
                await _clientService.UpdateInformationOfClient(identificationNumber, clientDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }

}

