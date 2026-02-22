using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Common.DTO
{
    public class CreateDriverDto
    {
        public string Firstname { get; set; } = default!;
        public string? Lastname { get; set; }
        public string Phonenumber { get; set; } = default!;
        public string Licensenumber { get; set; } = default!;
        
    }
    public class UpdateDriverDto
    {
        public string Firstname { get; set; } = default!;
        public string? Lastname { get; set; }
        public string Phonenumber { get; set; } = default!;
        public string Licensenumber { get; set; } = default!;
        public long? DriverStatusId { get; set; }
    }
    public class DriverResponseDto
    {
        public long Driverid { get; set; }
        public string Firstname { get; set; } = default!;
        public string? Lastname { get; set; }
        public string Phonenumber { get; set; } = default!;
        public string Licensenumber { get; set; } = default!;
        public long? DriverStatusId { get; set; }
        public string? DriverStatus { get; set; }
    }


}
