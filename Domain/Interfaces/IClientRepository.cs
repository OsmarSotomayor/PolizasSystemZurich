using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(string identificationNumber);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(string identificationNumber);
        Task<IEnumerable<Client>> FilterAsync(string? name, string? email, string? identificationNumber);
    }
}
