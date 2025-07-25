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
        Task<Policy?> GetByIdAsync(Guid id, bool track);
        Task AddAsync(Policy policy);
        Task UpdateAsync(Policy policy);
        Task<IEnumerable<Policy>> GetPoliciesByIdClient(int idClient, bool track);

        Task SaveAsync();

        Task<IEnumerable<Policy>> FilterPoliciesAsync(
        string? policyType = null,
        string? state = null,
        DateTime? startDateFrom = null,
        DateTime? startDateTo = null,
        DateTime? expirationDateFrom = null,
        DateTime? expirationDateTo = null);
    }
}
