using Backend.SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repo.Interface
{
    public interface IClientRepository
    {
        Task<Client> CreateAsync(Client client);
        Task<Client?> GetByIdAsync(long id);
        Task<List<Client>> GetAllAsync();
        Task<Client> UpdateAsync(Client client);
    }

}
