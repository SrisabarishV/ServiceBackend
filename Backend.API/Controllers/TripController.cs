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
    public class TripController : ControllerBase
    {
        private readonly ITripService _service;

        public TripController(ITripService service)
        {
            _service = service;
        }

        [HttpPost("CreateTrip")]
        public async Task<IActionResult> Create(CreateTripDto dto)
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

        [HttpGet("GetTripById")]
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

        [HttpGet("GetAllTrips")]
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

        [HttpPut("UpdateTrip")]
        public async Task<IActionResult> Update(long tripid, UpdateTripDto dto)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("UserId")!.Value);

                var result = await _service.UpdateAsync(tripid,dto, userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.FailResponse(ex.Message));
            }
        }

        [HttpDelete("DeleteTrip")]
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
