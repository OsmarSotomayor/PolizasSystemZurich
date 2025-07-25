using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientService
    {
        Task AddAsync(ClientCreateDto createDto);
        Task UpdateAsync(string identificationNumber, ClientUpdateDto updateDto);
        Task DeleteAsync(string identificationNumber);
        Task<ClientDto?> GetByIdAsync(string identificationNumber);

        Task<IEnumerable<ClientDto>> FilterAsync(string? name, string? email, int identificationNumber);

        Task<IEnumerable<ClientDto>> GetAllAsync();
    }
}
