using Backend.Common;
using Backend.Common.DTO;
using Backend.Repo.Interface;
using Backend.Service.Interface;
using Backend.SQLContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Implement
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repo;

        public ClientService(IClientRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse<ClientResponseDto>> CreateAsync(CreateClientDto dto, long createdBy)
        {
            try
            {
                var client = new Client
                {
                    Companyname = dto.Companyname,
                    Contactperson = dto.Contactperson,
                    Contactnumber = dto.Contactnumber,
                    Email = dto.Email,
                    Address = dto.Address,
                    Isactive = true,
                    Createdat = DateTime.UtcNow,
                    Createdby = createdBy
                };

                var result = await _repo.CreateAsync(client);

                return ApiResponse<ClientResponseDto>.SuccessResponse(Map(result), "Client created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<ClientResponseDto>.FailResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<ClientResponseDto>> GetByIdAsync(long id)
        {
            var client = await _repo.GetByIdAsync(id);

            if (client == null)
                return ApiResponse<ClientResponseDto>.FailResponse("Client not found");

            return ApiResponse<ClientResponseDto>.SuccessResponse(Map(client));
        }

        public async Task<ApiResponse<List<ClientResponseDto>>> GetAllAsync()
        {
            var clients = await _repo.GetAllAsync();

            var data = clients.Select(Map).ToList();

            return ApiResponse<List<ClientResponseDto>>.SuccessResponse(data);
        }

        public async Task<ApiResponse<ClientResponseDto>> UpdateAsync(long clientid,UpdateClientDto dto , long modifiedBy)
        {
            var client = await _repo.GetByIdAsync(clientid);

            if (client == null)
                return ApiResponse<ClientResponseDto>.FailResponse("Client not found");

            client.Companyname = dto.Companyname;
            client.Contactperson = dto.Contactperson;
            client.Contactnumber = dto.Contactnumber;
            client.Email = dto.Email;
            client.Address = dto.Address;
            client.Modifiedat = DateTime.UtcNow;
            client.Modifiedby = modifiedBy;

            var result = await _repo.UpdateAsync(client);

            return ApiResponse<ClientResponseDto>.SuccessResponse(Map(result), "Updated successfully");
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long id, long modifiedBy)
        {
            var success = await _repo.GetByIdAsync(id);

            if (success==null)
                return ApiResponse<bool>.FailResponse("Client not found");

            success.Isactive = false;
            success.Modifiedat = DateTime.UtcNow;
            success.Modifiedby = modifiedBy;

            await _repo.UpdateAsync(success);

            return ApiResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }

        private static ClientResponseDto Map(Client c)
        {
            return new ClientResponseDto
            {
                Clientid = c.Clientid,
                Companyname = c.Companyname,
                Contactperson = c.Contactperson,
                Contactnumber = c.Contactnumber,
                Email = c.Email,
                Address = c.Address
            };
        }
    }

}
