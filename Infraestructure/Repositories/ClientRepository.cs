using Domain.Interfaces;
using Domain.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class ClientRepository:IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(string identificationNumber)
        {
            return await _context.Clients.FindAsync(identificationNumber);
        }

        public async Task AddAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string identificationNumber)
        {
            var client = await _context.Clients.FindAsync(identificationNumber);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Client>> FilterAsync(string? name, string? email, string? identificationNumber)
        {
            var query = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.FullName.Contains(name));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(c => c.Email.Contains(email));

            if (!string.IsNullOrEmpty(identificationNumber))
                query = query.Where(c => c.IdentificationNumber.Contains(identificationNumber));

            return await query.ToListAsync();
        }
    }
}
