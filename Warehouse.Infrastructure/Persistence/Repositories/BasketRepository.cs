using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;
using Warehouse.Infrastructure.Persistence.Contexts;

namespace Warehouse.Infrastructure.Persistence.Repositories;
public class BasketRepository : GenericRepository<Basket>, IBasketRepository
{
    public BasketRepository(WarehouseDbContext context) : base(context)
    {
    }
    public async Task<Basket> GetSingleBasketByUserIdAsync(Guid userId)
    {
        var result = await _dbContext.Baskets
            .Include(b => b.BasketLines) 
            .FirstOrDefaultAsync(o => o.UserId == userId);

        if (result == null)
        {
            throw new BasketNotFoundException($"Basket for User with ID {userId} not found.");
        }

        return result;
    }

    public async Task<Basket> GetSingleBasketWithDetailsByUserIdAsync(Guid userId)
    {
        var result = await _dbContext.Baskets
            .Include(b => b.User)
            .Include(b => b.BasketLines)
                .ThenInclude(bl => bl.Product)
            .Include(p => p.BasketLines)
                .ThenInclude(x=>x.Size)
            .FirstOrDefaultAsync(o => o.UserId == userId);

        if (result == null)
        {
            throw new BasketNotFoundException($"Basket for User with ID {userId} not found.");
        }

        return result;
    }
}
