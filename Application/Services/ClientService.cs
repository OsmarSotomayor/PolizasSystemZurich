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

        public async Task<int> AddAsync(ClientCreateDto createDto)
        {
            var clientExists = await _clientRepository.GetByIdAsync(1, false);
            if (clientExists is not null)
                throw new Exception("Cliente ya existe");

            var client = _mapper.Map<Client>(createDto);
            await _clientRepository.AddAsync(client);

            return client.IdentificationNumber;
        }


        public async Task UpdateAsync(int identificationNumber, ClientUpdateDto updateDto)
        {
            var existingClient = await _clientRepository.GetByIdAsync(identificationNumber, false);
            if (existingClient == null)
                throw new Exception("Cliente no existe");

            _mapper.Map(updateDto, existingClient);
            await _clientRepository.UpdateAsync(existingClient);
        }

        public async Task DeleteAsync(int identificationNumber)
        {
            var client = await _clientRepository.GetByIdAsync(identificationNumber, false);
            if (client is null)
                throw new Exception("Cliente no existe");

            await _clientRepository.DeleteAsync(client);
        }

        public async Task<ClientDto?> GetByIdAsync(int identificationNumber)
        {
            var clientExists = await _clientRepository.GetByIdAsync(identificationNumber, false);
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
