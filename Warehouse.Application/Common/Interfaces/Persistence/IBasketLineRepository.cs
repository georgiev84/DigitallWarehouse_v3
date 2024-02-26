using Warehouse.Domain.Entities.Baskets;
using Warehouse.Persistence.Abstractions.Interfaces;

namespace Warehouse.Application.Common.Interfaces.Persistence;

public interface IBasketLineRepository : IGenericRepository<BasketLine>
{
    Task BulkDelete(Guid basketId);
}