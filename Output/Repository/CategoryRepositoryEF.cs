using InvoiceManagement.Contracts;
using InvoiceManagement.Data;
using InvoiceManagement.Dto;
using InvoiceManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Dapper;
namespace InvoiceManagement.Repositories;

public class CategoryRepositoryEF : BaseRepository, ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepositoryEF(AppDbContext dbContext, IConfiguration configuration) 
    :base(configuration)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CategoryEntity>> GetAllAsync()
    {
        return await _dbContext.Categories 
                             .AsNoTracking()
                             .ToListAsync();
    }

    public async Task<CategoryEntity?> GetByIdAsync(int id)
    {
        return await _dbContext.Categories
                             .AsNoTracking()
                             .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<CategoryEntity> AddAsync(CategoryEntity entity)
    {
        _dbContext.Categories.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(CategoryEntity entity)
    {
        _dbContext.Categories.Update(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbContext.Categories
                                      .FindAsync(id);
        if(entity == null)
            return false;
        //_dbContext.Categories
        //          .Remove(entity);
        entity.IsActive = false;
        entity.UpdatedBy = "SYSTEM";      // or current user
        entity.UpdatedDate = DateTime.Now;
        return await _dbContext.SaveChangesAsync() > 0;
    }
    public async Task<PagedResultDto<CategoryDto>> GetAllPagedAsync(CategorySearchDto request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@PageNumber", request.PageNumber);
        parameters.Add("@PageSize", request.PageSize);

    
            parameters.Add("@Code", request.Code);
        
            parameters.Add("@Name", request.Name);
        
            parameters.Add("@Description", request.Description);
        

        return await ExecutePagedAsync<CategoryDto>(
            "sp_Category_GetAllPaged",
            parameters,
            request.PageNumber,
            request.PageSize);
    }
}