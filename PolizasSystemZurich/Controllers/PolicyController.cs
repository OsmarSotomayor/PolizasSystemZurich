using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PolizasSystemZurich.Controllers
{
    [Route("api/policy")]
    [ApiController]
    public class PolicyController:ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            this._policyService = policyService;
        }

        /// <summary>
        /// Endpoint para asociar una poliza de seguro a un cliente
        /// </summary>
        /// <param name="policyDto"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PolicyCreateDto policyDto) //Admin
        {        
            try
            {
                var policiyId = await _policyService.AddAsync(policyDto);
                return Ok(policiyId);
            }
            catch (Exception ex)
            {
                return Conflict(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Consulta las polizas del cliente 
        /// </summary>
        /// <param name="clientId">identificador de 10 digitos del cliente</param>
        /// <returns></returns>
        [Authorize(Policy = "Client")]
        [HttpGet("identificador-cliente/{clientId}")]
        public async Task<IActionResult> GetPoliciesByClient([FromRoute] int clientId) //admin
        {
            try
            {
                var policies = await _policyService.GetPoliciesOfClient(clientId);
                return Ok(policies);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
               
        }

        /// <summary>
        /// Desactiva/Cancela una póliza de seguro
        /// </summary>
        /// <param name="idPolicy">ID único de la póliza (GUID)</param>
        /// <response code="200">Póliza cancelada exitosamente</response>
        /// <response code="400">La póliza ya está cancelada o no existe</response>
        [Authorize(Policy = "Client")]
        [HttpPatch("{idPolicy:guid}/cancelar")]
        public async Task<IActionResult> CancelPolicy([FromRoute] Guid idPolicy) //cliente
        {
            try
            {
                await _policyService.DesactivatePolicy(idPolicy);
                return Ok(new { Message = "Póliza cancelada exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Filtra pólizas por tipo, estado o rango de fechas
        /// </summary>
        /// <param name="type">Tipo de póliza (ej. "Auto", "Vida")</param>
        /// <param name="state">Estado de la póliza (ej. "Active", "Cancelled")</param>
        /// <param name="startDateFrom">Fecha de inicio desde (formato: yyyy-MM-dd)</param>
        /// <param name="startDateTo">Fecha de inicio hasta (formato: yyyy-MM-dd)</param>
        /// <param name="expirationDateFrom">Fecha de expiración desde (formato: yyyy-MM-dd)</param>
        /// <param name="expirationDateTo">Fecha de expiración hasta (formato: yyyy-MM-dd)</param>
        [Authorize(Policy = "Admin")]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterPolicies(  //admin
            [FromQuery] string? type = null,
            [FromQuery] string? state = null,
            [FromQuery] DateTime? startDateFrom = null,
            [FromQuery] DateTime? startDateTo = null,
            [FromQuery] DateTime? expirationDateFrom = null,
            [FromQuery] DateTime? expirationDateTo = null)
        {
            var policies = await _policyService.FilterPoliciesAsync(
                type,
                state,
                startDateFrom,
                startDateTo,
                expirationDateFrom,
                expirationDateTo);

            return Ok(policies);
        }
    }
}
