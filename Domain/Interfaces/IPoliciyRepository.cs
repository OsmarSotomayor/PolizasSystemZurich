using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPoliciyRepository
    {
        Task<IEnumerable<Policy>> GetAllAsync();
        Task<Policy?> GetByIdAsync(Guid id);
        Task AddAsync(Policy policy);
        Task UpdateAsync(Policy policy);
        Task<IEnumerable<Policy>> GetPoliciesByIdClient(int idClient, bool track);
    }
}
