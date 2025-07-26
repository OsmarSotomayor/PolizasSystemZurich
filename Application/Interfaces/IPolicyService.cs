using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPolicyService
    {
        Task<Guid> AddAsync(PolicyCreateDto createDto);

        Task<IEnumerable<PolicyResponseDto>> GetPoliciesOfClient(int identificationClient);

        Task DesactivatePolicy(Guid idPolicy);

        Task<IEnumerable<PolicyResponseDto>> FilterPoliciesAsync(
            string? policyType = null,
            string? state = null,
            DateTime? startDateFrom = null,
            DateTime? startDateTo = null,
            DateTime? expirationDateFrom = null,
            DateTime? expirationDateTo = null);
    }
}
