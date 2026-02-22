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
    public class BookingIntentController : ControllerBase
    {
        private readonly IBookingIntentService _service;

        public BookingIntentController(IBookingIntentService service)
        {
            _service = service;
        }

        [HttpPost("CreateBookingIntent")]
        public async Task<IActionResult> Create(CreateBookingIntentDto dto)
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

        [HttpGet("GetAllBookingIntent")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("IntentGetById")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("UpdateBookingIntent")]
        public async Task<IActionResult> Update(long id, UpdateBookingIntentDto dto)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("UserId")!.Value);
                var result = await _service.UpdateAsync(id, dto, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.FailResponse(ex.Message));
            }
        }

        [HttpDelete("DeleteIntent")]
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
