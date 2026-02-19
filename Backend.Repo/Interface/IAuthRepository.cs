using Backend.SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repo.Interface
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmailAsync(string email);

        
        Task<string?> GetAppSettingValueAsync(string key);
    }

}
