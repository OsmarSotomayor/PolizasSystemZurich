using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PolicyService:IPolicyService
    {
        private readonly IPoliciyRepository _policiyRepository;
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;

        public PolicyService(IPoliciyRepository policiyRepository, IMapper mapper, IClientRepository clientRepository)
        {
            this._policiyRepository = policiyRepository;
            this._mapper = mapper;
            this._clientRepository = clientRepository;
        }

        public async Task<Guid> AddAsync(PolicyCreateDto createDto)
        {
            if (createDto.ExpirationDate < createDto.StartDate)
                throw new Exception("Fecha de expiracion no puede ser mayor a fecha inicio");

            var client = await _clientRepository.GetByIdAsync(createDto.ClientIdentificator, false);
            if (client is null)
                throw new Exception("Cliente no existe");

            var policy = _mapper.Map<Policy>(createDto);
            policy.ClientId = createDto.ClientIdentificator;
            await _policiyRepository.AddAsync(policy);
            return policy.Id;
        }

        public async Task<IEnumerable<PolicyResponseDto>> GetPoliciesOfClient(int identificationClient)
        {
            var client = await _clientRepository.GetByIdAsync(identificationClient, false);
            if (client is null)
                throw new Exception("Cliente no existe");

            var policies = await  _policiyRepository.GetPoliciesByIdClient(identificationClient, false);
            return _mapper.Map<IEnumerable<PolicyResponseDto>>(policies);
        }

        public async Task DesactivatePolicy(Guid idPolicy)
        {
            var policiy = await _policiyRepository.GetByIdAsync(idPolicy, true);
            if (policiy.State == "Cancelada")
                throw new Exception("Poliza esta cancelada");

            policiy.State = "Cancelada";
            await _policiyRepository.SaveAsync();
        }

        public async Task<IEnumerable<PolicyResponseDto>> FilterPoliciesAsync(
            string? policyType = null,
            string? state = null,
            DateTime? startDateFrom = null,
            DateTime? startDateTo = null,
            DateTime? expirationDateFrom = null,
            DateTime? expirationDateTo = null)
        {
            var policies = await _policiyRepository.FilterPoliciesAsync(
                    policyType,
                    state,
                    startDateFrom,
                    startDateTo,
                    expirationDateFrom,
                    expirationDateTo);

            return _mapper.Map<IEnumerable<PolicyResponseDto>>(policies);
        }

    }
}
