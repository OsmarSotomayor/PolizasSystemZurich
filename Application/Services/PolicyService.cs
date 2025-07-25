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

        public async Task AddAsync(PolicyCreateDto createDto)
        {
            if (createDto.ExpirationDate < createDto.StartDate)
                throw new Exception("Fecha de expiracion no puede ser mayor a fecha inicio");

            var client = await _clientRepository.GetByIdAsync(createDto.ClientIdentificator, false);
            if (client is null)
                throw new Exception("Cliente no existe");

            var policy = _mapper.Map<Policy>(createDto);
            policy.ClientId = createDto.ClientIdentificator;
            await _policiyRepository.AddAsync(policy);
        }

        public async Task<IEnumerable<PolicyResponseDto>> GetPoliciesOfClient(int identificationClient)
        {
            var client = await _clientRepository.GetByIdAsync(identificationClient, false);
            if (client is null)
                throw new Exception("Cliente no existe");

            var policies = await  _policiyRepository.GetPoliciesByIdClient(identificationClient, false);
            return _mapper.Map<IEnumerable<PolicyResponseDto>>(policies);
        }

    }
}
