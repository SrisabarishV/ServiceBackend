using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Common.DTO
{
    public class CreateTruckDto
    {
        public string RegistrationNumber { get; set; } = null!;
        public string TruckType { get; set; } = null!;
        public decimal? CapacityMt { get; set; }
        
    }
    public class UpdateTruckDto
    {
        public string RegistrationNumber { get; set; } = null!;
        public string TruckType { get; set; } = null!;
        public decimal? CapacityMt { get; set; }
        public long? TruckStatusId { get; set; }
        public bool Isactive { get; set; }
    }
    public class TruckResponseDto
    {
        public long TruckId { get; set; }
        public string RegistrationNumber { get; set; } = null!;
        public string TruckType { get; set; } = null!;
        public decimal? CapacityMt { get; set; }
        public long? TruckStatusId { get; set; }
        public string? TruckStatus { get; set; }
    }
}
