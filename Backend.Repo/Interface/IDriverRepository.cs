using Backend.SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repo.Interface
{
    public interface IDriverRepository
    {
        Task<Driver> CreateAsync(Driver driver);
        Task<Driver?> GetByIdAsync(long id);
        Task<List<Driver>> GetAllAsync();
        Task<Driver> UpdateAsync(Driver driver);
    }
}
