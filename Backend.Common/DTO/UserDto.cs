using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Common.DTO
{
    public class CreateUserDto
    {
        public string Firstname { get; set; } = default!;
        public string? Lastname { get; set; }
        public string? Phonenumber { get; set; }
        public string Emailid { get; set; } = default!;
        public string Password { get; set; } = default!;
        public long? Roleid { get; set; }
    }


    public class UpdateUserDto
    {
        
        public string Firstname { get; set; } = default!;
        public string? Lastname { get; set; }
        public string? Phonenumber { get; set; }
        public long? Roleid { get; set; }
    }

    public class UserResponseDto
    {
        public long Userid { get; set; }
        public string Firstname { get; set; } = default!;
        public string? Lastname { get; set; }
        public string Emailid { get; set; } = default!;
        public string? Phonenumber { get; set; }
        public long? Roleid { get; set; }
        public string? Rolename { get; set; }

    }


}
