using Backend.Common;
using Backend.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Interface
{
    public interface ITripService
    {
        Task<ApiResponse<TripResponseDto>> CreateAsync(CreateTripDto dto, long userId);
        Task<ApiResponse<TripResponseDto>> GetByIdAsync(long id);
        Task<ApiResponse<List<TripResponseDto>>> GetAllAsync();
        Task<ApiResponse<TripResponseDto>> UpdateAsync(long tripid, UpdateTripDto dto, long userId);
        Task<ApiResponse<bool>> DeleteAsync(long id, long userId);
    }

}
