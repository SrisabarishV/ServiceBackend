using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Common.DTO;

namespace Backend.Common.DTO
{
    public class CreateTripDto
    {
        public long Intentid { get; set; }
        public long? Truckid { get; set; }
        public long? Driverid { get; set; }

        public bool IsExternalHire { get; set; }

        public string? ExternalTransporterName { get; set; }
        public string? ExternalTruckReg { get; set; }
        public string? ExternalTruckType { get; set; }
        public string? ExternalDriverName { get; set; }
        public string? ExternalDriverPhone { get; set; }
    }
    public class UpdateTripDto
    {
        
        public long? Truckid { get; set; }
        public long? Driverid { get; set; }

        public long? TripStatusId { get; set; }
        public int? ProgressPercentage { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
    public class TripResponseDto
    {
        public long Tripid { get; set; }
        public string TripNumber { get; set; } = default!;

        public IntentDto Intent { get; set; } 
        public TruckDto? Truck { get; set; }
        public DriverDto? Driver { get; set; }
        public bool IsExternalHire { get; set; }

        public string? ExternalTransporterName { get; set; }
        public string? ExternalTruckReg { get; set; }
        public string? ExternalTruckType { get; set; }
        public string? ExternalDriverName { get; set; }
        public string? ExternalDriverPhone { get; set; }
        public long? TripStatusId { get; set; }
        public int? ProgressPercentage { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
    public class IntentDto
    {
        public long Intentid { get; set; }
        public string IntentNumber { get; set; } = default!;
        public string PickupLocation { get; set; } = null!;
        public string DeliveryLocation { get; set; } = null!;
    }

    public class TruckDto
    {
        public long Truckid { get; set; }
        public string? TruckNumber { get; set; }
        public decimal? CapacityMt { get; set; }
    }

    public class DriverDto
    {
        public long Driverid { get; set; }
        public string? Firstname { get; set; }
        public string Phonenumber { get; set; } = default!;
        public string Licensenumber { get; set; } = default!;
    }


}
