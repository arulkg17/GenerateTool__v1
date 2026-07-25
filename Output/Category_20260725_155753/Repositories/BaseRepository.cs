using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using InvoiceManagement.DTOs;

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
        var connectionString = _configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("DefaultConnection is missing.");
    }
    protected virtual async Task<PagedResultDto<TDto>> ExecutePagedAsync<TDto>
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