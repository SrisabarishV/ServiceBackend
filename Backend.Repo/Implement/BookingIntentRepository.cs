using Backend.Repo.Interface;
using Backend.SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repo.Implement
{
    public class BookingIntentRepository : IBookingIntentRepository
    {
        private readonly AppDbContext _context;

        public BookingIntentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BookingIntent?> GetByIdAsync(long id)
        {
            return await _context.BookingIntents
                .Include(x => x.IntStatus)
                .Include(x => x.PodStatus)
                .FirstOrDefaultAsync(x => x.Intentid == id && x.Isactive);
        }

        public async Task<List<BookingIntent>> GetAllAsync()
        {
            return await _context.BookingIntents
                .Include(x=>x.IntStatus)
                 .Include(x => x.PodStatus)
                .Where(x => x.Isactive)
                .ToListAsync();
        }

        public async Task AddAsync(BookingIntent entity)
        {
            await _context.BookingIntents.AddAsync(entity);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(BookingIntent entity)
        {
            _context.BookingIntents.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
