using Backend.Common;
using Backend.Common.DTO;
using Backend.Common.Enum;
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
    public class BookingIntentService : IBookingIntentService
    {
        private readonly IBookingIntentRepository _repo;

        public BookingIntentService(IBookingIntentRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse<object>> CreateAsync(CreateBookingIntentDto dto, long userId)
        {
            try
            {
                var entity = new BookingIntent
                {
                    Clientid = dto.Clientid,
                    IntentAmount = dto.IntentAmount,
                    TruckType = dto.TruckType,
                    OperationMode = dto.OperationMode,
                    PickupLocation = dto.PickupLocation,
                    DeliveryLocation = dto.DeliveryLocation,
                    Material = dto.Material,
                    WeightMt = dto.WeightMt,
                    OperationalComments = dto.OperationalComments,
                    IntStatusId = (long)IntentStatus.PendingApproval,
                    PodStatusId = (long)PodStatus.Pending,
                    Isactive = true,
                    Createdat = DateTime.UtcNow,
                    Createdby = userId
                };

                await _repo.AddAsync(entity);   // After this EF will populate Intentid

                entity.IntentNumber = $"INT-{entity.Intentid}";

                await _repo.UpdateAsync(entity);

                return ApiResponse<object>.SuccessResponse("Booking intent created successfully");
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
                var list = await _repo.GetAllAsync();

                var data = list.Select(x => new BookingIntentResponseDto
                {
                    Intentid = x.Intentid,
                    IntentNumber = x.IntentNumber,
                    Clientid = x.Clientid,
                    IntentAmount = x.IntentAmount,
                    TruckType = x.TruckType,
                    OperationMode = x.OperationMode,
                    PickupLocation = x.PickupLocation,
                    DeliveryLocation = x.DeliveryLocation,
                    Material = x.Material,
                    WeightMt = x.WeightMt,
                    OperationalComments = x.OperationalComments,
                    IntStatusId = x.IntStatusId,
                    IntStatusName = x.IntStatus?.StatusName,   

                    PodStatusId = x.PodStatusId,
                    PodStatusName = x.PodStatus?.StatusName    
                });

                return ApiResponse<object>.SuccessResponse(data);
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.FailResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<object>> GetByIdAsync(long id)
        {
            try
            {
                var x = await _repo.GetByIdAsync(id);

                if (x == null)
                    return ApiResponse<object>.FailResponse("Booking intent not found");

                var data = new BookingIntentResponseDto
                {
                    Intentid = x.Intentid,
                    IntentNumber = x.IntentNumber,
                    Clientid = x.Clientid,
                    IntentAmount = x.IntentAmount,
                    TruckType = x.TruckType,
                    OperationMode = x.OperationMode,
                    PickupLocation = x.PickupLocation,
                    DeliveryLocation = x.DeliveryLocation,
                    Material = x.Material,
                    WeightMt = x.WeightMt,
                    OperationalComments = x.OperationalComments,
                    IntStatusId = x.IntStatusId,
                    IntStatusName = x.IntStatus?.StatusName,   

                    PodStatusId = x.PodStatusId,
                    PodStatusName = x.PodStatus?.StatusName   
                };

                return ApiResponse<object>.SuccessResponse(data);
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.FailResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<object>> UpdateAsync(long id, UpdateBookingIntentDto dto, long userId)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);

                if (entity == null)
                    return ApiResponse<object>.FailResponse("Booking intent not found");

               
                entity.Clientid = dto.Clientid;
                entity.IntentAmount = dto.IntentAmount;
                entity.TruckType = dto.TruckType;
                entity.OperationMode = dto.OperationMode;
                entity.PickupLocation = dto.PickupLocation;
                entity.DeliveryLocation = dto.DeliveryLocation;
                entity.Material = dto.Material;
                entity.WeightMt = dto.WeightMt;
                entity.OperationalComments = dto.OperationalComments;
                entity.IntStatusId = dto.IntStatusId;
                entity.PodStatusId = dto.PodStatusId;
                entity.Modifiedat = DateTime.UtcNow;
                entity.Modifiedby = userId;

                await _repo.UpdateAsync(entity);

                return ApiResponse<object>.SuccessResponse("Booking intent updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.FailResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<object>> DeleteAsync(long id, long userId)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);

                if (entity == null)
                    return ApiResponse<object>.FailResponse("Booking intent not found");

                entity.Isactive = false;
                entity.Modifiedat = DateTime.UtcNow;
                entity.Modifiedby = userId;

                await _repo.UpdateAsync(entity);

                return ApiResponse<object>.SuccessResponse("Booking intent deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.FailResponse(ex.Message);
            }
        }
    }
}
