using Backend.Common.DTO;
using Backend.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
            private readonly IAuthService _service;

            public AuthController(IAuthService service)
            {
                _service = service;
            }

            // ================= LOGIN =================

            [HttpPost("login")]
           
            public async Task<IActionResult> Login(LoginRequestDto request)
            {
                var result = await _service.LoginAsync(request);

                if (result == null)
                    return Unauthorized("Invalid email or password");

                return Ok(result);
            }

            // ================= LOGOUT =================

            [HttpPost("logout")]
            
            public async Task<IActionResult> Logout()
            {
                var token = Request.Headers["Authorization"]
                    .ToString()
                    .Replace("Bearer ", "");

                await _service.LogoutAsync(token);

            return Ok(new
            {
                success = true,
                message = "Logged out successfully"
            });
        }
        }

    
}
