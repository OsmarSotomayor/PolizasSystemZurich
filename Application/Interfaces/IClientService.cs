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
        Task<int> AddAsync(ClientCreateDto createDto);
        Task UpdateAsync(int identificationNumber, ClientUpdateDto updateDto);
        Task DeleteAsync(int identificationNumber);
        Task<ClientDto?> GetByIdAsync(int identificationNumber);

        Task<IEnumerable<ClientDto>> FilterAsync(string? name, string? email, int identificationNumber);

        Task<IEnumerable<ClientDto>> GetAllAsync();
    }
}
