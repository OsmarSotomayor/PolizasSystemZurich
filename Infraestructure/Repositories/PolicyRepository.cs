using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class PolicyRepository:RepositoryBase<Policy>, IPoliciyRepository
    {
        private readonly AppDbContext _context;

        public PolicyRepository(AppDbContext context) : base(context) 
        {    
            _context = context;
        }

        public async Task<IEnumerable<Policy>> GetAllAsync()
            => await _context.Policies.ToListAsync();

        public async Task<Policy?> GetByIdAsync(Guid id, bool track)
            => await this.FindByCondition(pol => pol.Id == id, track).FirstOrDefaultAsync();

        public async Task AddAsync(Policy policy)
        {
            this.Create(policy);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Policy policy)
        {
            _context.Policies.Update(policy);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Policy>> GetPoliciesByIdClient(int idClient, bool track)
        {
            return await this.FindByCondition(pol => pol.ClientId == idClient, track).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Policy>> FilterPoliciesAsync(
        string? policyType = null,
        string? state = null,
        DateTime? startDateFrom = null,
        DateTime? startDateTo = null,
        DateTime? expirationDateFrom = null,
        DateTime? expirationDateTo = null)
        {
            var query = _context.Policies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(policyType))
                query = query.Where(p => p.Type.Contains(policyType));

            if (!string.IsNullOrWhiteSpace(state))
                query = query.Where(p => p.State == state);

            if (startDateFrom.HasValue)
                query = query.Where(p => p.StartDate >= startDateFrom.Value);

            if (startDateTo.HasValue)
                query = query.Where(p => p.StartDate <= startDateTo.Value);

            if (expirationDateFrom.HasValue)
                query = query.Where(p => p.ExpirationDate >= expirationDateFrom.Value);

            if (expirationDateTo.HasValue)
                query = query.Where(p => p.ExpirationDate <= expirationDateTo.Value);

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
