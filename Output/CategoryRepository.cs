using Microsoft.EntityFrameworkCore;
using InvoiceManagement.Data;
using InvoiceManagement.Contracts;
using InvoiceManagement.Entities;

namespace InvoiceManagement.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CategoryEntity>> GetAllAsync()
    {
        return await _dbContext.Categorys.ToListAsync();
    }

    public async Task<CategoryEntity?> GetByIdAsync(int id)
    {
        return await _dbContext.Categorys.FindAsync(id);
    }

    public async Task<CategoryEntity> AddAsync(CategoryEntity entity)
    {
        _dbContext.Categorys.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(CategoryEntity entity)
    {
        _dbContext.Categorys.Update(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbContext.Categorys.FindAsync(id);
        if (entity == null)
            return false;

        _dbContext.Categorys.Remove(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}