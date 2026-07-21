using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using <Your Namespace>.Contracts;
using <Your Namespace>.DTOs;
using <Your Namespace>.Models;

namespace <Your Namespace>.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
[ApiVersion("1.0")]
public class Controller : ControllerBase
{
    private readonly IService _service;
    private readonly ILogger<Controller> _logger;

    public Controller(
        IService service,
        ILogger<Controller> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            _logger.LogInformation("GetAll  started");

            var data = await _service.GetAllAsync();

            _logger.LogInformation("GetAll  completed successfully");

            return Ok(new ApiResponse<IEnumerable<Dto>>
            {
                Success = true,
                Message = " retrieved successfully",
                Data = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetAll ");

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error retrieving ",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            _logger.LogInformation("GetById  started for Id: {Id}", id);

            var item = await _service.GetByIdAsync(id);

            if (item == null)
            {
                _logger.LogWarning(" not found for Id: {Id}", id);

                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = " not found"
                });
            }

            return Ok(new ApiResponse<Dto>
            {
                Success = true,
                Message = " retrieved successfully",
                Data = item
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetById  for Id: {Id}", id);

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error retrieving ",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(Dto dto)
    {
        try
        {
            _logger.LogInformation("Create  started");

            var result = await _service.AddAsync(dto);

            _logger.LogInformation("Create  completed");

            return Ok(new ApiResponse<Dto>
            {
                Success = true,
                Message = " created successfully",
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Create ");

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error creating ",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(Dto dto, int id)
    {
        try
        {
            _logger.LogInformation("Update  started for Id: {Id}", id);

            dto.Id = id;

            var updated = await _service.UpdateAsync(dto);

            if (!updated)
            {
                _logger.LogWarning("Update failed -  not found for Id: {Id}", id);

                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = " not found"
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = " updated successfully"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Update  for Id: {Id}", id);

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error updating ",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            _logger.LogInformation("Delete  started for Id: {Id}", id);

            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                _logger.LogWarning("Delete failed -  not found for Id: {Id}", id);

                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = " not found"
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = " deleted successfully"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Delete  for Id: {Id}", id);

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error deleting ",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }
    [HttpGet("paged")]
    public async Task<IActionResult> GetAllPaged([FromQuery]PagedRequestDto request)
    {
        var result =   await _service.GetAllPagedAsync(request);
        return Ok(result);
    }

}
