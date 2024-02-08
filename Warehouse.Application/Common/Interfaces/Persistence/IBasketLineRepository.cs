using Warehouse.Domain.Entities;

namespace Warehouse.Application.Common.Interfaces.Persistence;
public interface IBasketLineRepository : IGenericRepository<BasketLine>
{
    Task<BasketLine> GetBasketByUserId(Guid userId);
}
