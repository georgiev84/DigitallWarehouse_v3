using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Baskets;
using Warehouse.Domain.Exceptions.BasketExceptions;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.PostgreSQL.Persistence.Contexts;

namespace Warehouse.Persistence.PostgreSQL.Persistence.Repositories;

public class BasketRepository : GenericRepository<Basket>, IBasketRepository
{
    public BasketRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Basket> GetSingleBasketByUserIdAsync(Guid userId)
    {
        try
        {
            return await _dbContext.Set<Basket>()
                .Include(b => b.BasketLines)
                .SingleAsync(o => o.UserId == userId);
        }
        catch (Exception ex)
        {
            throw new BasketNotFoundException($"Basket for User with ID {userId} not found.", ex);
        }
    }

    public async Task<Basket> GetSingleBasketWithDetailsByUserIdAsync(Guid userId)
    {
        try
        {
            var result = await _dbContext.Set<Basket>()
                .Include(b => b.User)
                .Include(b => b.BasketLines)
                    .ThenInclude(bl => bl.Product)
                .Include(p => p.BasketLines)
                    .ThenInclude(x => x.Size)
                .SingleAsync(o => o.UserId == userId);

            return result;
        }
        catch (InvalidOperationException ex)
        {
            throw new BasketNotFoundException($"Basket for User with ID {userId} not found.", ex);
        }
        catch
        {
            throw;
        }
    }
}