using Backend.Common;
using Backend.Common.Enum;
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
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _repo;

        public DriverService(IDriverRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse<DriverResponseDto>> CreateAsync(CreateDriverDto dto, long createdBy)
        {
            try
            {
                var driver = new Driver
                {
                    Firstname = dto.Firstname,
                    Lastname = dto.Lastname,
                    Phonenumber = dto.Phonenumber,
                    Licensenumber = dto.Licensenumber,
                    DriverStatusId = (long)DriverStatus.Available,
                    Isactive = true,
                    Createdat = DateTime.UtcNow,
                    Createdby = createdBy 
                };

                var result = await _repo.CreateAsync(driver);

                return ApiResponse<DriverResponseDto>.SuccessResponse(Map(result), "Driver created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<DriverResponseDto>.FailResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<DriverResponseDto>> GetByIdAsync(long id)
        {
            var driver = await _repo.GetByIdAsync(id);

            if (driver == null)
                return ApiResponse<DriverResponseDto>.FailResponse("Driver not found");

            return ApiResponse<DriverResponseDto>.SuccessResponse(Map(driver));
        }

        public async Task<ApiResponse<List<DriverResponseDto>>> GetAllAsync()
        {
            var drivers = await _repo.GetAllAsync();

            var data = drivers.Select(Map).ToList();

            return ApiResponse<List<DriverResponseDto>>.SuccessResponse(data);
        }

        public async Task<ApiResponse<DriverResponseDto>> UpdateAsync(long driverid, UpdateDriverDto dto, long modifiedBy)
        {
            var driver = await _repo.GetByIdAsync(driverid);

            if (driver == null)
                return ApiResponse<DriverResponseDto>.FailResponse("Driver not found");

            if (!Enum.IsDefined(typeof(DriverStatus), dto.DriverStatusId))
            {
                return ApiResponse<DriverResponseDto>.FailResponse("Invalid Driver Status. Must be 50 (Available), 51 (On Trip), or 52 (On Leave).");
            }

            driver.Firstname = dto.Firstname;
            driver.Lastname = dto.Lastname;
            driver.Phonenumber = dto.Phonenumber;
            driver.Licensenumber = dto.Licensenumber;
            driver.DriverStatusId = dto.DriverStatusId;
            driver.Modifiedat = DateTime.UtcNow;
            driver.Modifiedby = modifiedBy; 

            var result = await _repo.UpdateAsync(driver);

            return ApiResponse<DriverResponseDto>.SuccessResponse(Map(result), "Updated successfully");
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long id, long modifiedBy)
        {
            var success = await _repo.GetByIdAsync(id);

            if (success==null)
                return ApiResponse<bool>.FailResponse("Driver not found");
            success.Isactive = false;
            success.Modifiedat = DateTime.UtcNow;
            success.Modifiedby = modifiedBy;

            await _repo.UpdateAsync(success);

            return ApiResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }

        private static DriverResponseDto Map(Driver d)
        {
            return new DriverResponseDto
            {
                Driverid = d.Driverid,
                Firstname = d.Firstname,
                Lastname = d.Lastname,
                Phonenumber = d.Phonenumber,
                Licensenumber = d.Licensenumber,
                DriverStatusId = d.DriverStatusId,
                DriverStatus=d.DriverStatus?.StatusName
            };
        }
    }

}
