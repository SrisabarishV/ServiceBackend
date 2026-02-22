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
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }

       
        [HttpPost("CreateClient")]
        public async Task<IActionResult> Create(CreateClientDto dto)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("UserId")!.Value);

                var result = await _service.CreateAsync(dto , userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.FailResponse(ex.Message));
            }
        }

        [HttpGet("GetByClientId")]
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

        [HttpGet("GetAllClient")]
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

        [HttpPut("UpdateClient")]
        public async Task<IActionResult> Update(long clientid,UpdateClientDto dto)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("UserId")!.Value);

                var result = await _service.UpdateAsync(clientid,dto, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.FailResponse(ex.Message));
            }
        }

        [HttpDelete("DeleteClient")]
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
