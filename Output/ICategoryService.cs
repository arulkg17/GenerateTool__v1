using InvoiceManagement.DTOs;

namespace InvoiceManagement.Contracts;
public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto> GetByIdAsync(int id);
    Task<CategoryDto> AddAsync(CategoryDto dto);
    Task<bool> UpdateAsync(CategoryDto dto);
    Task<bool> DeleteAsync(int id);
    Task<PagedResultDto<CategoryDto>> GetAllPagedAsync(
		string? code,
		string? name,
		string? description,
		int pageNumber,
		int pageSize);
}