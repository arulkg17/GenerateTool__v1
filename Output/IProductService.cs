using <Your Namespace>.DTOs;

namespace <Your Namespace>.Contracts;
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto> GetByIdAsync(int id);
    Task<ProductDto> AddAsync(ProductDto dto);
    Task<bool> UpdateAsync(ProductDto dto);
    Task<bool> DeleteAsync(int id);
}