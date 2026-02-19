using Backend.Common;
using Backend.Common.DTO;
using Backend.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId")?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Ok(ApiResponse<object>.FailResponse("Invalid token"));
                }

                var userId = long.Parse(userIdClaim);

                var result = await _service.CreateAsync(dto, userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.FailResponse(ex.Message));
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("GetAllUser")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Update(UpdateUserDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Ok(ApiResponse<object>.FailResponse("Invalid token"));
            }
            var userId = long.Parse(userIdClaim);

            var result = await _service.UpdateAsync(dto, userId);

            return Ok(result);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> Delete(long id)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Ok(ApiResponse<object>.FailResponse("Invalid token"));
            }
            var userId = long.Parse(userIdClaim);

            var result = await _service.DeleteAsync(id, userId);

            return Ok(result);
        }
    }
}
