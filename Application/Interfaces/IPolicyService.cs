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
        Task AddAsync(PolicyCreateDto createDto);

        Task<IEnumerable<PolicyResponseDto>> GetPoliciesOfClient(int identificationClient);
    }
}
