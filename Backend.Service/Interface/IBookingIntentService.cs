using Backend.Common;
using Backend.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Interface
{
    public interface IBookingIntentService
    {
        Task<ApiResponse<object>> CreateAsync(CreateBookingIntentDto dto, long userId);
        Task<ApiResponse<object>> GetAllAsync();
        Task<ApiResponse<object>> GetByIdAsync(long id);
        Task<ApiResponse<object>> UpdateAsync(long id, UpdateBookingIntentDto dto, long userId);
        Task<ApiResponse<object>> DeleteAsync(long id, long userId);
    }
}
