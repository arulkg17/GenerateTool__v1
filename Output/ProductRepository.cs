using Microsoft.EntityFrameworkCore;
using <Your Namespace>.Data;
using <Your Namespace>.Contracts;
using <Your Namespace>.Entities;

namespace <Your Namespace>.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<ProductEntity?> GetByIdAsync(int id)
    {
        return await _dbContext.Products.FindAsync(id);
    }

    public async Task<ProductEntity> AddAsync(ProductEntity entity)
    {
        _dbContext.Products.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(ProductEntity entity)
    {
        _dbContext.Products.Update(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbContext.Products.FindAsync(id);
        if (entity == null)
            return false;

        _dbContext.Products.Remove(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}