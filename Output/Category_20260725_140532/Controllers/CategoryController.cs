using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Asp.Versioning;
using InvoiceManagement.Contracts;
using InvoiceManagement.DTOs;
using InvoiceManagement.Models;

namespace InvoiceManagement.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
[ApiVersion("1.0")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(
        ICategoryService service,
        ILogger<CategoryController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            _logger.LogInformation("GetAll Category started");

            var data = await _service.GetAllAsync();

            _logger.LogInformation("GetAll Category completed successfully");

            return Ok(new ApiResponse<IEnumerable<CategoryDto>>
            {
                Success = true,
                Message = "Category retrieved successfully",
                Data = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetAll Category");

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error retrieving Category",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }

    [HttpGet("GetById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            _logger.LogInformation("GetById Category started for Id: {Id}", id);

            var item = await _service.GetByIdAsync(id);

            if (item == null)
            {
                _logger.LogWarning("Category not found for Id: {Id}", id);

                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Category not found"
                });
            }

            return Ok(new ApiResponse<CategoryDto>
            {
                Success = true,
                Message = "Category retrieved successfully",
                Data = item
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetById Category for Id: {Id}", id);

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error retrieving Category",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }

    [HttpPost("Create")] 
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CategoryDto dto)
    {
        try
        {
            _logger.LogInformation("Create Category started");

            var result = await _service.AddAsync(dto);

            _logger.LogInformation("Create Category completed");

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                new ApiResponse<CategoryDto>
                {
                    Success = true,
                    Message = "Category created successfully",
                    Data = result
                });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Create Category");

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error creating Category",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }

    [HttpPut("Update/{id}")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryDto dto)
    {
        try
        {
            _logger.LogInformation("Update Category started for Id: {Id}", id);

            dto.Id = id;

            var updated = await _service.UpdateAsync(dto);

            if (!updated)
            {
                _logger.LogWarning("Update failed - Category not found for Id: {Id}", id);

                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Category not found"
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Category updated successfully"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Update Category for Id: {Id}", id);

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error updating Category",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }

    [HttpDelete("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            _logger.LogInformation("Delete Category started for Id: {Id}", id);

            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                _logger.LogWarning("Delete failed - Category not found for Id: {Id}", id);

                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Category not found"
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Category deleted successfully"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Delete Category for Id: {Id}", id);

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error deleting Category",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }
    [HttpGet("GetAllPaged")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPaged([FromQuery] CategorySearchDto request)
    {
        try
        {
            _logger.LogInformation(
                "GetAllPaged Category started PageNumber:{PageNumber}, PageSize:{PageSize}",
                request.PageNumber,
                request.PageSize);
            var result = await _service.GetAllPagedAsync(request);
           
            return Ok(new ApiResponse<PagedResultDto<CategoryDto>>
            {
                Success = true,
                Message = "Category retrieved successfully",
                Data = result
            });
        }
        catch(Exception ex)
        {
            _logger.LogError(ex,
                "Error in GetAllPaged Category");

            return StatusCode(500,
                new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error retrieving Category",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
        }
    }
}
