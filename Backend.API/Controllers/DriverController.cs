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
    [Authorize]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _service;

        public DriverController(IDriverService service)
        {
            _service = service;
        }

        [HttpPost("CreateDriver")]
        public async Task<IActionResult> Create(CreateDriverDto dto)
        {
            try
            {
               
                var userId = long.Parse(User.FindFirst("UserId")!.Value);

                var result = await _service.CreateAsync(dto, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.FailResponse(ex.Message));
            }
        }

        [HttpGet("GetByDriverID")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.FailResponse(ex.Message));
            }
        }

        [HttpGet("GetAllDriver")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.FailResponse(ex.Message));
            }
        }

        [HttpPut("UpdateDriver")]
        public async Task<IActionResult> Update(long driverid ,UpdateDriverDto dto)
        {
            try
            {
                
                var userId = long.Parse(User.FindFirst("UserId")!.Value);

               
                var result = await _service.UpdateAsync(driverid, dto, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.FailResponse(ex.Message));
            }
        }

        [HttpDelete("DeleteDriver")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("UserId")!.Value);

                var result = await _service.DeleteAsync(id, userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.FailResponse(ex.Message));
            }
        }
    }

}
