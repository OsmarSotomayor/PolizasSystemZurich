using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class ClientRepository: RepositoryBase<Client> ,IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context): base(context) {
        
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await this.FindAll(false).ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int identificationNumber, bool track)
        {
            return await this.FindByCondition(cli => cli.IdentificationNumber == identificationNumber, track).FirstOrDefaultAsync(); 
        }

        public async Task AddAsync(Client client)
        {
            client.IdentificationNumber = GenerateUnique10DigitNumber();
            this.Create(client);
            await _context.SaveChangesAsync();
        }

        private int GenerateUnique10DigitNumber()
        {
            var rnd = new Random();
            int number;
            do
            {
                number = rnd.Next(1000000000, 2000000000); // Genera entre 1.000.000.000 y 1.999.999.999
            } while (_context.Clients.Any(c => c.IdentificationNumber == number));

            return number;
        }
        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client client)
        {
            this.Delete(client);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> FilterAsync(string? name, string? email, int identificationNumber)
        {
            var query = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.FullName.Contains(name));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(c => c.Email.Contains(email));

            if (identificationNumber != null)
                query = query.Where(c => c.IdentificationNumber ==identificationNumber);

            return await query.ToListAsync();
        }
    }
}
