using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClientService: IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(ClientCreateDto createDto)
        {
            var clientExists = await _clientRepository.GetByIdAsync(createDto.IdentificationNumber);
            if (clientExists is not null)
                throw new Exception("Cliente ya existe");

            var client = _mapper.Map<Client>(createDto);
            await _clientRepository.AddAsync(client);
        }

        public async Task UpdateAsync(string identificationNumber, ClientUpdateDto updateDto)
        {
            var existingClient = await _clientRepository.GetByIdAsync(identificationNumber);
            if (existingClient == null)
                throw new Exception("Cliente ya existe");

            _mapper.Map(updateDto, existingClient);
            await _clientRepository.UpdateAsync(existingClient);
        }

        public async Task DeleteAsync(string identificationNumber)
        {
            var client = await _clientRepository.GetByIdAsync(identificationNumber);
            if (client is null)
                throw new Exception("Cliente no existe");

            await _clientRepository.DeleteAsync(identificationNumber);
        }

        public async Task<ClientDto?> GetByIdAsync(string identificationNumber)
        {
            var clientExists = await _clientRepository.GetByIdAsync(identificationNumber);
            if (clientExists == null)
                throw new Exception("Cliente no existe");
            return _mapper.Map<ClientDto>(clientExists);
        }

        public async Task<IEnumerable<ClientDto>> FilterAsync(string? name, string? email, int identificationNumber)
        {
            var filteredClients = await _clientRepository.FilterAsync(name, email, identificationNumber);
            return _mapper.Map<IEnumerable<ClientDto>>(filteredClients);
        }

        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }
    }
}
