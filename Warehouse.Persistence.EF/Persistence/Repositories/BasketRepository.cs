using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.EF.Persistence.Contexts;

namespace Warehouse.Persistence.EF.Persistence.Repositories;

public class BasketRepository : GenericRepository<Basket>, IBasketRepository
{
    public BasketRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Basket> GetSingleBasketByUserIdAsync(Guid userId)
    {
        var result = await _dbContext.Set<Basket>()
            .Include(b => b.BasketLines)
            .SingleAsync(o => o.UserId == userId);

        if (result == null)
        {
            throw new BasketNotFoundException($"Basket for User with ID {userId} not found.");
        }

        return result;
    }

    public async Task<Basket> GetSingleBasketWithDetailsByUserIdAsync(Guid userId)
    {
        var result = await _dbContext.Set<Basket>()
            .Include(b => b.User)
            .Include(b => b.BasketLines)
                .ThenInclude(bl => bl.Product)
            .Include(p => p.BasketLines)
                .ThenInclude(x => x.Size)
            .SingleAsync(o => o.UserId == userId);

        if (result == null)
        {
            throw new BasketNotFoundException($"Basket for User with ID {userId} not found.");
        }

        return result;
    }
}