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
    public class TruckController : ControllerBase
    {
        private readonly ITruckService _service;

        public TruckController(ITruckService service)
        {
            _service = service;
        }

        [HttpPost("CreateTruck")]
        public async Task<IActionResult> Create(CreateTruckDto dto)
        {
            var userId = long.Parse(User.FindFirst("UserId")!.Value);
            var result = await _service.CreateAsync(dto, userId);
            return Ok(result);
        }

        [HttpPut("UpdateTruck")]
        public async Task<IActionResult> Update(long truckId, UpdateTruckDto dto)
        {
            var userId = long.Parse(User.FindFirst("UserId")!.Value);
            var result = await _service.UpdateAsync(truckId, dto, userId);
            return Ok(result);
        }

        [HttpGet("GetAllTruck")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("TruckGetById")]
        public async Task<IActionResult> GetById(long truckId)
        {
            var result = await _service.GetByIdAsync(truckId);
            return Ok(result);
        }

        [HttpDelete("DeleteTruck")]
        public async Task<IActionResult> Delete(long truckId)
        {
            var userId = long.Parse(User.FindFirst("UserId")!.Value);
            var result = await _service.DeleteAsync(truckId, userId);
            return Ok(result);
        }
    }
}
