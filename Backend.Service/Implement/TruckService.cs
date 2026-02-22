using Backend.Common;
using Backend.Common.DTO;
using Backend.Common.Enum;
using Backend.Repo.Interface;
using Backend.Service.Interface;
using Backend.SQLContext;
using Backend.SQLContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Implement
{
    public class TruckService : ITruckService
    {
        private readonly ITruckRepository _repo;

        public TruckService(ITruckRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse<object>> CreateAsync(CreateTruckDto dto, long userId)
        {
            try
            {
                var truck = new Truck
                {
                    RegistrationNumber = dto.RegistrationNumber,
                    TruckType = dto.TruckType,
                    CapacityMt = dto.CapacityMt,
                    TruckStatusId = (long)TruckStatus.Available,
                    Isactive = true,
                    Createdat = DateTime.UtcNow,
                    Createdby = userId
                };

                await _repo.AddAsync(truck);

                return ApiResponse<object>.SuccessResponse("Truck created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.FailResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<object>> UpdateAsync(long truckId, UpdateTruckDto dto, long userId)
        {
            try
            {
                var truck = await _repo.GetByIdAsync(truckId);

                if (truck == null)
                    return ApiResponse<object>.FailResponse("Truck not found");

                truck.RegistrationNumber = dto.RegistrationNumber;
                truck.TruckType = dto.TruckType;
                truck.CapacityMt = dto.CapacityMt;
                truck.TruckStatusId = dto.TruckStatusId;
                truck.Modifiedat = DateTime.UtcNow;
                truck.Modifiedby = userId;

                await _repo.UpdateAsync(truck);

                return ApiResponse<object>.SuccessResponse("Truck updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.FailResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<object>> GetAllAsync()
        {
            try
            {
                var trucks = await _repo.GetAllAsync();

                var data = trucks.Select(x => new TruckResponseDto
                {
                    TruckId = x.Truckid,
                    RegistrationNumber = x.RegistrationNumber,
                    TruckType = x.TruckType,
                    CapacityMt = x.CapacityMt,
                    TruckStatusId = x.TruckStatusId,
                    TruckStatus = x.TruckStatus?.StatusName
                    
                });

                return ApiResponse<object>.SuccessResponse(data);
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.FailResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<object>> GetByIdAsync(long truckId)
        {
            try
            {
                var x = await _repo.GetByIdAsync(truckId);

                if (x == null)
                    return ApiResponse<object>.FailResponse("Truck not found");

                var data = new TruckResponseDto
                {
                    TruckId = x.Truckid,
                    RegistrationNumber = x.RegistrationNumber,
                    TruckType = x.TruckType,
                    CapacityMt = x.CapacityMt,
                    TruckStatusId = x.TruckStatusId,
                    TruckStatus = x.TruckStatus?.StatusName
                };

                return ApiResponse<object>.SuccessResponse(data);
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.FailResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<object>> DeleteAsync(long truckId, long userId)
        {
            try
            {
                var truck = await _repo.GetByIdAsync(truckId);

                if (truck == null)
                    return ApiResponse<object>.FailResponse("Truck not found");

                truck.Isactive = false;
                truck.Modifiedat = DateTime.UtcNow;
                truck.Modifiedby = userId;

                await _repo.UpdateAsync(truck);

                return ApiResponse<object>.SuccessResponse("Truck deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.FailResponse(ex.Message);
            }
        }
    }
}
