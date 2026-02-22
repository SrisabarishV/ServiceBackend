using Backend.SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repo.Interface
{
    public interface ITruckRepository
    {
        Task<Truck?> GetByIdAsync(long truckId);
        Task<List<Truck>> GetAllAsync();
        Task AddAsync(Truck truck);
        Task UpdateAsync(Truck truck);
    }
}
