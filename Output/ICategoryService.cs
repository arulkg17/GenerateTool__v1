using InvoiceManagement.DTOs;

namespace InvoiceManagement.Contracts;
public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto> GetByIdAsync(int id);
    Task<CategoryDto> AddAsync(Dto dto);
    Task<bool> UpdateAsync(CategoryDto dto);
    Task<bool> DeleteAsync(int id);
}