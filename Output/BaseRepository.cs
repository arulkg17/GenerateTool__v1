using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace InvoiceManagement.Repositories;

public abstract class BaseRepository
{
    private readonly IConfiguration _configuration;

    protected BaseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected IDbConnection CreateConnection()
    {
        return new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection"));
    }
    
    protected async Task<PagedResultDto<TDto>> ExecutePagedAsync<TDto>(
        string storedProcedure,
        DynamicParameters parameters,
        int pageNumber,
        int pageSize)
    {
        using var connection = CreateConnection();

        using var multi = await connection.QueryMultipleAsync(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);

        var items = (await multi.ReadAsync<TDto>()).ToList();
        var totalRecords = await multi.ReadFirstAsync<int>();

        return new PagedResultDto<TDto>
        {
            Items = items,
            TotalRecords = totalRecords,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

}