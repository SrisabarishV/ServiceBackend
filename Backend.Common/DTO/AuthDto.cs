using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Common.DTO
{
    public class LoginRequestDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
        public long UserId { get; set; }
        public long? RoleId { get; set; }
    }

}
