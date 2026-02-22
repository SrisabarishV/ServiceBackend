using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Common.DTO
{
    public class CreateClientDto
    {
        public string Companyname { get; set; } = default!;
        public string? Contactperson { get; set; }
        public string? Contactnumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
    public class UpdateClientDto
    {
        
        public string Companyname { get; set; } = default!;
        public string? Contactperson { get; set; }
        public string? Contactnumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
    public class ClientResponseDto
    {
        public long Clientid { get; set; }
        public string Companyname { get; set; } = default!;
        public string? Contactperson { get; set; }
        public string? Contactnumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }


}
