using Backend.Repo.Interface;
using Backend.SQLContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repo.Implement
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Client> CreateAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client?> GetByIdAsync(long id)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(x => x.Clientid == id && x.Isactive);
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Clients
                .Where(x => x.Isactive)
                .ToListAsync();
        }

        public async Task<Client> UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return client;
        }

    
    }

}
