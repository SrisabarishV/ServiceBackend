using Backend.Common;
using Backend.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Interface
{
    public interface IUserService
    {
        Task<ApiResponse<UserResponseDto>> CreateAsync(CreateUserDto dto);
        Task<ApiResponse<UserResponseDto>> GetByIdAsync(long id);
        Task<ApiResponse<List<UserResponseDto>>> GetAllAsync();
        Task<ApiResponse<string>> UpdateAsync(UpdateUserDto dto, long modifiedBy);
        Task<ApiResponse<string>> DeleteAsync(long id, long modifiedBy);
    }
}