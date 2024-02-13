using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.EF.Persistence.Contexts;

namespace Warehouse.Persistence.EF.Persistence.Repositories;

public class BasketLineRepository : GenericRepository<BasketLine>, IBasketLineRepository
{
    public BasketLineRepository(WarehouseDbContext dbContext) : base(dbContext)
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