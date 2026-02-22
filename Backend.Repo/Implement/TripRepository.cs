using Backend.Repo.Interface;
using Backend.SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.Repo.Implement
{
    public class TripRepository : ITripRepository
    {
        private readonly AppDbContext _context;

        public TripRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Trip> CreateAsync(Trip trip)
        {
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
            return trip;
        }

        public async Task<Trip?> GetByIdAsync(long id)
        {
            return await _context.Trips
                .Include(x => x.Intent)
                .Include(x => x.Truck)
                .Include(x => x.Driver)
                .FirstOrDefaultAsync(x => x.Tripid == id && x.Isactive);
        }


        public async Task<List<Trip>> GetAllAsync()
        {
            return await _context.Trips
                .Include(x => x.Intent)
                .Include(x => x.Truck)
                .Include(x => x.Driver)
                .Where(x => x.Isactive)
                .ToListAsync();
        }

        public async Task<Trip> UpdateAsync(Trip trip)
        {
            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();
            return trip;
        }

    }

}
