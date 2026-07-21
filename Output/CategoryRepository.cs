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

    public async Task<PagedResultDto<CategoryEntity>>
        GetAllPagedAsync(
        PagedRequestDto request)
        {


        var parameters = new[]
        {

        new SqlParameter(
        "@SearchText",
        request.SearchText ?? (object)DBNull.Value),


        new SqlParameter(
        "@PageNumber",
        request.PageNumber),


        new SqlParameter(
        "@PageSize",
        request.PageSize)

        };



        var result =
        await _dbContext
        .
        .FromSqlRaw(
        "EXEC sp_Category_GetAllPaged @SearchText,@PageNumber,@PageSize",
        parameters)
        .ToListAsync();



        var total =
        result.Count();



        return new PagedResultDto<CategoryEntity>
        {

        Items=result,

        PageNumber=request.PageNumber,

        PageSize=request.PageSize,

        TotalRecords=total

        };

}
}