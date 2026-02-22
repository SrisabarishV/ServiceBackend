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
    public class TripService : ITripService
    {
        private readonly ITripRepository _repo;

        public TripService(ITripRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse<TripResponseDto>> CreateAsync(CreateTripDto dto, long userId)
        {
            try
            {
                var entity = new Trip
                {
                    Intentid = dto.Intentid,
                    Truckid = dto.Truckid == 0 ? null : dto.Truckid,
                    Driverid = dto.Driverid == 0 ? null : dto.Driverid,

                    IsExternalHire = dto.IsExternalHire,
                    ExternalTransporterName = dto.ExternalTransporterName,
                    ExternalTruckReg = dto.ExternalTruckReg,
                    ExternalTruckType = dto.ExternalTruckType,
                    ExternalDriverName = dto.ExternalDriverName,
                    ExternalDriverPhone = dto.ExternalDriverPhone,

                    TripStatusId = (long)TripStatus.Pending,
                    
                    Isactive = true,
                    Createdat = DateTime.UtcNow,
                    Createdby = userId
                };

                var result = await _repo.CreateAsync(entity);

                result.TripNumber =$"TRIP-{result.Tripid:D8}";

                await _repo.UpdateAsync(result);
                var fullTrip = await _repo.GetByIdAsync(result.Tripid);

                return ApiResponse<TripResponseDto>.SuccessResponse(Map(fullTrip), "Trip created");
            }
            catch (Exception ex)
            {
                return ApiResponse<TripResponseDto>.FailResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<TripResponseDto>> GetByIdAsync(long id)
        {
            var trip = await _repo.GetByIdAsync(id);

            if (trip == null)
                return ApiResponse<TripResponseDto>.FailResponse("Trip not found");

            return ApiResponse<TripResponseDto>.SuccessResponse(Map(trip));
        }

        public async Task<ApiResponse<List<TripResponseDto>>> GetAllAsync()
        {
            var trips = await _repo.GetAllAsync();

            var data = trips.Select(Map).ToList();

            return ApiResponse<List<TripResponseDto>>.SuccessResponse(data);
        }

        public async Task<ApiResponse<TripResponseDto>> UpdateAsync(long tripid, UpdateTripDto dto, long userId)
        {
            try
            {
                var trip = await _repo.GetByIdAsync(tripid);

                if (trip == null)
                    return ApiResponse<TripResponseDto>.FailResponse("Trip not found");

                trip.Truckid = dto.Truckid;
                trip.Driverid = dto.Driverid;
                trip.TripStatusId = dto.TripStatusId;

                // ================= STATUS BASED LOGIC =================

                if (dto.TripStatusId.HasValue)
                {
                    switch ((TripStatus)dto.TripStatusId.Value)
                    {
                        case TripStatus.Pending:
                            trip.ProgressPercentage = 25;
                            break;

                        case TripStatus.Assigned:
                            trip.ProgressPercentage = 50;
                            break;

                        case TripStatus.InTransit:
                            trip.ProgressPercentage = 75;

                            if (trip.StartTime == null)
                                trip.StartTime = DateTime.UtcNow;

                            break;

                        case TripStatus.Delivered:
                            trip.ProgressPercentage = 100;

                            if (trip.EndTime == null)
                                trip.EndTime = DateTime.UtcNow;

                            break;
                    }
                }

                // =======================================================

                trip.Modifiedat = DateTime.UtcNow;
                trip.Modifiedby = userId;

                var result = await _repo.UpdateAsync(trip);

                return ApiResponse<TripResponseDto>.SuccessResponse(Map(result), "Updated");
            }
            catch (Exception ex)
            {
                return ApiResponse<TripResponseDto>.FailResponse(ex.Message);
            }
        }


        public async Task<ApiResponse<bool>> DeleteAsync(long id, long userId)
        {
            try
            {
                var trip = await _repo.GetByIdAsync(id);

                if (trip == null)
                    return ApiResponse<bool>.FailResponse("Trip not found");

                if (trip.TripStatusId == (long)TripStatus.InTransit ||
                    trip.TripStatusId == (long)TripStatus.Delivered)
                {
                    return ApiResponse<bool>.FailResponse("Trip cannot be deleted");
                }

                trip.Isactive = false;
                trip.Modifiedat = DateTime.UtcNow;
                trip.Modifiedby = userId;

                await _repo.UpdateAsync(trip);

                return ApiResponse<bool>.SuccessResponse(true, "Deleted");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.FailResponse(ex.Message);
            }
        }

        private static TripResponseDto Map(Trip t)
        {
            return new TripResponseDto
            {
                Tripid = t.Tripid,
                TripNumber = t.TripNumber,

                Intent = new IntentDto
                {
                    Intentid = t.Intent.Intentid,
                    IntentNumber = t.Intent.IntentNumber,
                    DeliveryLocation = t.Intent.DeliveryLocation,
                    PickupLocation = t.Intent.PickupLocation

                },

                Truck = t.Truck == null ? null : new TruckDto
                {
                    Truckid = t.Truck.Truckid,
                    TruckNumber = t.Truck.RegistrationNumber,
                    CapacityMt = t.Truck.CapacityMt
                },

                Driver = t.Driver == null ? null : new DriverDto
                {
                    Driverid = t.Driver.Driverid,
                    Firstname = t.Driver.Firstname,
                    Phonenumber = t.Driver.Phonenumber,
                    Licensenumber = t.Driver.Licensenumber
                },

                IsExternalHire =t.IsExternalHire,
                ExternalDriverName =t.ExternalDriverName,
                ExternalDriverPhone = t.ExternalDriverPhone,
                ExternalTransporterName =t.ExternalTransporterName,
                ExternalTruckReg =t.ExternalTruckReg,
                ExternalTruckType = t.ExternalTruckType,

                TripStatusId = t.TripStatusId,
                ProgressPercentage = t.ProgressPercentage,
                StartTime = t.StartTime,
                EndTime = t.EndTime
            };
        }
    }

}
