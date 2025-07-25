using Application.Dtos;
using Application.Interfaces;
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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PolicyCreateDto policyDto) //Admin
        {
            await _policyService.AddAsync(policyDto);
            return Ok();
        }

        /// <summary>
        /// Consulta las polizas del cliente 
        /// </summary>
        /// <param name="clientId">identificador de 10 digitos del cliente</param>
        /// <returns></returns>
        [HttpGet("identificador-cliente/{clientId}")]
        public async Task<IActionResult> GetPoliciesByClient([FromRoute] int clientId)
        {
            var policies = await _policyService.GetPoliciesOfClient(clientId);
            return Ok(policies);    
        }
    }
}
