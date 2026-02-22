using Backend.Repo.Interface;
using Backend.SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repo.Implement
{
    public class TruckRepository : ITruckRepository
    {
        private readonly AppDbContext _context;

        public TruckRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Truck?> GetByIdAsync(long truckId)
        {
            return await _context.Trucks
                .Include(x => x.TruckStatus)
                .FirstOrDefaultAsync(x => x.Truckid == truckId && x.Isactive);
        }

        public async Task<List<Truck>> GetAllAsync()
        {
            return await _context.Trucks
                .Include(x => x.TruckStatus)
                .Where(x => x.Isactive)
                .ToListAsync();
        }

        public async Task AddAsync(Truck truck)
        {
            await _context.Trucks.AddAsync(truck);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Truck truck)
        {
            _context.Trucks.Update(truck);
            await _context.SaveChangesAsync();
        }
    }
}
