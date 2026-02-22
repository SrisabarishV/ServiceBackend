using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Common.DTO
{
    public class CreateBookingIntentDto
    {
       
        public long Clientid { get; set; }
        public decimal IntentAmount { get; set; }
        public string? TruckType { get; set; }
        public string? OperationMode { get; set; }
        public string PickupLocation { get; set; } = null!;
        public string DeliveryLocation { get; set; } = null!;
        public string? Material { get; set; }
        public decimal? WeightMt { get; set; }
        public string? OperationalComments { get; set; }
       
    }
    public class UpdateBookingIntentDto
    {
        
        public long Clientid { get; set; }
        public decimal IntentAmount { get; set; }
        public string? TruckType { get; set; }
        public string? OperationMode { get; set; }
        public string PickupLocation { get; set; } = null!;
        public string DeliveryLocation { get; set; } = null!;
        public string? Material { get; set; }
        public decimal? WeightMt { get; set; }
        public string? OperationalComments { get; set; }
        public long? IntStatusId { get; set; }
        public long? PodStatusId { get; set; }
    }
    public class BookingIntentResponseDto
    {
        public long Intentid { get; set; }
        public string IntentNumber { get; set; } = null!;
        public long Clientid { get; set; }
        public decimal IntentAmount { get; set; }
        public string? TruckType { get; set; }
        public string? OperationMode { get; set; }
        public string PickupLocation { get; set; } = null!;
        public string DeliveryLocation { get; set; } = null!;
        public string? Material { get; set; }
        public decimal? WeightMt { get; set; }
        public string? OperationalComments { get; set; }
        public long? IntStatusId { get; set; }
        public string? IntStatusName { get; set; } 
        public long? PodStatusId { get; set; }
        public string? PodStatusName { get; set; }  
    }
}
