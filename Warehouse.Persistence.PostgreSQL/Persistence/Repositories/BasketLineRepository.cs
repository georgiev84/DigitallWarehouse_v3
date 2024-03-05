using Microsoft.EntityFrameworkCore;
using System.Data;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Baskets;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.PostgreSQL.Persistence.Contexts;

namespace Warehouse.Persistence.PostgreSQL.Persistence.Repositories;

public class BasketLineRepository : GenericRepository<BasketLine>, IBasketLineRepository
{
    public BasketLineRepository(WarehouseDbContext dbContext, IDbConnection dbConnection) : base(dbContext, dbConnection)
    {
    }

    public async Task BulkDelete(Guid basketId)
    {
        var basketLinesToRemove = await _dbContext.Set<BasketLine>()
            .Where(bl => bl.BasketId == basketId)
            .ToListAsync();

        _dbContext.Set<BasketLine>().RemoveRange(basketLinesToRemove);
    }
}