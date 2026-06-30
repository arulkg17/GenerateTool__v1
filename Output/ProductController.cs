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
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    private readonly ILogger<ProductController> _logger;

    public ProductController(
        IProductService service,
        ILogger<ProductController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            _logger.LogInformation("GetAll Product started");

            var data = await _service.GetAllAsync();

            _logger.LogInformation("GetAll Product completed successfully");

            return Ok(new ApiResponse<IEnumerable<ProductDto>>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in GetAll Product");

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error retrieving Product",
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
            _logger.LogInformation("GetById Product started for Id: {Id}", id);

            var item = await _service.GetByIdAsync(id);

            if (item == null)
            {
                _logger.LogWarning("Product not found for Id: {Id}", id);

                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Product not found"
                });
            }

            return Ok(new ApiResponse<ProductDto>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = item
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetById Product for Id: {Id}", id);

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error retrieving Product",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(ProductDto dto)
    {
        try
        {
            _logger.LogInformation("Create Product started");

            var result = await _service.AddAsync(dto);

            _logger.LogInformation("Create Product completed");

            return Ok(new ApiResponse<ProductDto>
            {
                Success = true,
                Message = "Product created successfully",
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Create Product");

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error creating Product",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(ProductDto dto, int id)
    {
        try
        {
            _logger.LogInformation("Update Product started for Id: {Id}", id);

            dto.Id = id;

            var updated = await _service.UpdateAsync(dto);

            if (!updated)
            {
                _logger.LogWarning("Update failed - Product not found for Id: {Id}", id);

                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Product not found"
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Product updated successfully"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Update Product for Id: {Id}", id);

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error updating Product",
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
            _logger.LogInformation("Delete Product started for Id: {Id}", id);

            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                _logger.LogWarning("Delete failed - Product not found for Id: {Id}", id);

                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Product not found"
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Product deleted successfully"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Delete Product for Id: {Id}", id);

            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Error deleting Product",
                Error = new ApiError
                {
                    Code = "500",
                    Details = ex.Message
                }
            });
        }
    }
}
