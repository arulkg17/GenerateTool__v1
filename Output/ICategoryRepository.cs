using InvoiceManagement.Entities;

namespace InvoiceManagement.Contracts;
public interface ICategoryRepository
{
	Task<IEnumerable<CategoryEntity>> GetAllAsync();
	Task<CategoryEntity> GetByIdAsync(int id);
	Task<CategoryEntity> AddAsync(CategoryEntity entity);
	Task<bool> UpdateAsync(Entity entity);
	Task<bool> DeleteAsync(int id);
}