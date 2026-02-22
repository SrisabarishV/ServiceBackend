using Backend.Common;
using Backend.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Interface
{
    public interface ITruckService
    {
        Task<ApiResponse<object>> CreateAsync(CreateTruckDto dto, long userId);
        Task<ApiResponse<object>> UpdateAsync(long truckId, UpdateTruckDto dto, long userId);
        Task<ApiResponse<object>> GetAllAsync();
        Task<ApiResponse<object>> GetByIdAsync(long truckId);
        Task<ApiResponse<object>> DeleteAsync(long truckId, long userId);
    }
}
