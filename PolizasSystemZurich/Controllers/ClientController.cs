using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PolizasSystemZurich.Controllers
{
    [ApiController]
    [Route("api/clientes")]
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
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientCreateDto clientDto) 
        {
            try
            {
                int key = await _clientService.AddAsync(clientDto);
                return CreatedAtAction(nameof(GetById), new { identificador = key }, clientDto);
            }
            catch (Exception ex) 
            {
                return Conflict(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza cliente
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="clientDto"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpPut("{identificador}")]
        public async Task<IActionResult> Update(int identificador, [FromBody] ClientUpdateDto clientDto)
        {
            try
            {
                await _clientService.UpdateAsync(identificador, clientDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina cliente
        /// </summary>
        /// <param name="identificador"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpDelete("{identificador}")]
        public async Task<IActionResult> Delete(int identificador) //admin
        {
            try
            {
                await _clientService.DeleteAsync(identificador);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            
        }

        /// <summary>
        /// Consulta cliente por Id
        /// </summary>
        /// <param name="identificador"></param>
        /// <returns></returns>
        [HttpGet("{identificador}")]
        public async Task<ActionResult<ClientDto>> GetById(int identificador) //admin
        {
            try
            {
                var client = await _clientService.GetByIdAsync(identificador);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            
        }
        /// <summary>
        /// Filtra clientes
        /// </summary>
        /// <param name="name"> nombre del cliente</param>
        /// <param name="email"> email del clienye</param>
        /// <param name="identificador"> identificador unico </param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ClientDto>>> Filter( //admin
            [FromQuery] string? name,
            [FromQuery] string? email,
            [FromQuery] int identificador)
        {
            try
            {
                var clients = await _clientService.FilterAsync(name, email, identificador);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Consulta todos los clientes
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll() //admin
        {
            try
            {
                var clients = await _clientService.GetAllAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza información específica del cliente (dirección y teléfono)
        /// </summary>
        /// <param name="identificationNumber">Número de identificación del cliente (10 dígitos)</param>
        /// <param name="clientDto">Datos a actualizar</param>
        /// <response code="204">Actualización exitosa</response>
        /// <response code="404">Cliente no encontrado</response>
        [Authorize(Policy = "Client")]
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

