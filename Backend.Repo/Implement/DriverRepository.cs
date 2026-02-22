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
    public class DriverRepository : IDriverRepository
    {
        private readonly AppDbContext _context;

        public DriverRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Driver> CreateAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<Driver?> GetByIdAsync(long id)
        {
            return await _context.Drivers
                .Include(d => d.DriverStatus)
                .FirstOrDefaultAsync(x => x.Driverid == id && x.Isactive);
        }

        public async Task<List<Driver>> GetAllAsync()
        {
            return await _context.Drivers
                .Include(d => d.DriverStatus)
                .Where(x => x.Isactive)
                .ToListAsync();
        }

        public async Task<Driver> UpdateAsync(Driver driver)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
            return driver;
        }
    }

}
