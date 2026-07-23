public class CategoryRepository 
       : ICategoryRepository
{
    private readonly AppDbContext _dbContext;


    public CategoryRepository(
        AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<CategoryEntity>> GetAllAsync()
    {
        return await _dbContext.
                             .ToListAsync();
    }


    public async Task<CategoryEntity?> GetByIdAsync(int id)
    {
        return await _dbContext.
                             .FindAsync(id);
    }


    public async Task<CategoryEntity> AddAsync(
        CategoryEntity entity)
    {
        _dbContext..Add(entity);

        await _dbContext.SaveChangesAsync();

        return entity;
    }


    public async Task<bool> UpdateAsync(
        CategoryEntity entity)
    {
        _dbContext..Update(entity);

        return await _dbContext.SaveChangesAsync() > 0;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbContext.
                                      .FindAsync(id);

        if(entity == null)
            return false;


        _dbContext.
                  .Remove(entity);


        return await _dbContext.SaveChangesAsync() > 0;
    }
     public async Task<PagedResultDto<CategoryDto>> GetAllPagedAsync(CategorySearchDto request)
     {

        var parameters = new DynamicParameters();


        parameters.Add("@PageNumber",
        request.PageNumber);


        parameters.Add("@PageSize",
        request.PageSize);


        parameters.Add("@CategoryName",
        request.CategoryName);


        parameters.Add("@Description",
        request.Description);

        var result =
        await connection.QueryAsync<CategoryDto>
        (
        "sp_Category_GetAllPaged",
        parameters,
        commandType: CommandType.StoredProcedure
        );

    }    
}