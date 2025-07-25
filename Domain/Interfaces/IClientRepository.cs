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
        Task<Client?> GetByIdAsync(int identificationNumber, bool track);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Client client);
        Task<IEnumerable<Client>> FilterAsync(string? name, string? email, int? identificationNumber);
        Task<IEnumerable<Client>> GetPolicies(int identificationNumber);

        Task SaveAsync();
    }
}
