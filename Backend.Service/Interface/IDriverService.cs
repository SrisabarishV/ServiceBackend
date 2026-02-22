using Backend.Common;
using Backend.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Interface
{
    public interface IDriverService
    {
        Task<ApiResponse<DriverResponseDto>> CreateAsync(CreateDriverDto dto, long createdBy);
        Task<ApiResponse<DriverResponseDto>> GetByIdAsync(long id);
        Task<ApiResponse<List<DriverResponseDto>>> GetAllAsync();
        Task<ApiResponse<DriverResponseDto>> UpdateAsync(long driverid ,UpdateDriverDto dto, long modifiedBy);
        Task<ApiResponse<bool>> DeleteAsync(long id, long modifiedBy);
    }
}
