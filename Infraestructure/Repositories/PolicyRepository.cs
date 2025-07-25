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

        public async Task<Policy?> GetByIdAsync(Guid id)
            => await _context.Policies.FindAsync(id);

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
    }
}
