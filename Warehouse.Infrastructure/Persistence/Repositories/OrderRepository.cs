using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;
using Warehouse.Infrastructure.Persistence.Contexts;

namespace Warehouse.Infrastructure.Persistence.Repositories;
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<Order> GetSingleOrderAsync(Guid orderId)
    {
        var result = await _dbContext.Orders
            .Include(o => o.User)
            .Include(o=> o.Status)
            .Include(o => o.OrderLines)
                .ThenInclude(od=>od.Product)
                .ThenInclude(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
            .Where(p => p.IsDeleted == false)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (result == null)
        {
            throw new OrderNotFoundException($"Order with ID {orderId} not found.");
        }

        return result;
    }

    public async Task<IEnumerable<Order>> GetOrdersListAsync()
    {
        var result = await _dbContext.Orders
            .Include(o => o.User)
            .Include(o => o.Status)
            .Where(p => p.IsDeleted == false)
            .ToListAsync();

        return result;
    }
}
