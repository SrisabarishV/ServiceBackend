using Backend.Common;
using Backend.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Interface
{
    public interface IClientService
    {
        Task<ApiResponse<ClientResponseDto>> CreateAsync(CreateClientDto dto, long createdBy);
        Task<ApiResponse<ClientResponseDto>> GetByIdAsync(long id);
        Task<ApiResponse<List<ClientResponseDto>>> GetAllAsync();
        Task<ApiResponse<ClientResponseDto>> UpdateAsync(long clientid,UpdateClientDto dto, long modifiedBy);
        Task<ApiResponse<bool>> DeleteAsync(long id, long modifiedBy);
    }

}
