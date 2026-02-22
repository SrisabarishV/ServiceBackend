using Backend.SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repo.Interface
{
    public interface ITripRepository
    {
        Task<Trip> CreateAsync(Trip trip);
        Task<Trip?> GetByIdAsync(long id);
        Task<List<Trip>> GetAllAsync();
        Task<Trip> UpdateAsync(Trip trip);
    }

}
