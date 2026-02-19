using Backend.SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repo.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(long id);
        Task<List<User>> GetAllAsync();
        Task<User> CreateAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> SoftDeleteAsync(User user);
        Task<bool> EmailExistsAsync(string email);
    }

}
