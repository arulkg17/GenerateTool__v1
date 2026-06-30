using <Your Namespoace>.Entities;

namespace <Your Namespace>.Contracts;
public interface IProductRepository
{
	Task<IEnumerable<ProductEntity>> GetAllAsync();
	Task<ProductEntity> GetByIdAsync(int id);
	Task<ProductEntity> AddAsync(ProductEntity entity);
	Task<bool> UpdateAsync(ProductEntity entity);
	Task<bool> DeleteAsync(int id);
}