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
        //var result = await _dbContext.Baskets
        //    .Include(b => b.User)
        //    //.Include(b => b..Status)
        //    .Include(b => b.BasketLines)
        //        .ThenInclude(bl => bl.Product)
        //        .ThenInclude(p => p.ProductSizes)
        //        .ThenInclude(ps => ps.Size)
        //    //.Where(p => p.IsDeleted == false)
        //    .FirstOrDefaultAsync(o => o.UserId == userId);

        var result = await _dbContext.Baskets.FirstOrDefaultAsync(o => o.UserId == userId);

        if (result == null)
        {
            throw new BasketNotFoundException($"Basket with ID {userId} not found.");
        }

        return result;
    }
}
