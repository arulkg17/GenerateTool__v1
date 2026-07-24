using InvoiceManagement.Entities;

namespace InvoiceManagement.Contracts;

public interface ICategoryRepository
{
	Task<IEnumerable<CategoryEntity>> GetAllAsync();
	Task<CategoryEntity?> GetByIdAsync(int id);
	Task<CategoryEntity> AddAsync(CategoryEntity entity);
	Task<bool> UpdateAsync(CategoryEntity entity);
	Task<bool> DeleteAsync(int id);
	Task<PagedResultDto<CategoryEntity>> GetAllPagedAsync(
		string? code,
		string? name,
		string? description,
		int pageNumber,
		int pageSize);
}