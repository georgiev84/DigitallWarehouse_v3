using Dapper;
using System.Data;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Products;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.PostgreSQL.Configuration.Contstants;
using Warehouse.Persistence.PostgreSQL.Persistence.Contexts;

namespace Warehouse.Persistence.PostgreSQL.Persistence.Repositories;

public class SizeRepository : GenericRepository<Size>, ISizeRepository
{
    public SizeRepository(WarehouseDbContext dbContext, IDbConnection dbConnection) : base(dbContext, dbConnection)
    {
    }
    public async Task<IEnumerable<string>> GetSizeNamesAsync()
    {
        try
        {
            return await _dbConnection.QueryAsync<string>(DapperConstants.GetSizeNames);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while fetching size names.", ex);
        }
    }
}