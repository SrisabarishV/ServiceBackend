using Backend.SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repo.Interface
{
    public interface IBookingIntentRepository
    {
        Task<BookingIntent?> GetByIdAsync(long id);
        Task<List<BookingIntent>> GetAllAsync();
        Task AddAsync(BookingIntent entity);
        Task UpdateAsync(BookingIntent entity);
    }
}
