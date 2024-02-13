using Warehouse.Domain.Entities;
using Warehouse.Persistence.Abstractions.Interfaces;

namespace Warehouse.Application.Common.Interfaces.Persistence;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersListAsync();

    Task<Order> GetSingleOrderAsync(Guid id);
}