using Warehouse.Domain.Entities;

namespace Warehouse.Application.Common.Interfaces.Persistence;
public interface IBasketRepository : IGenericRepository<Basket>
{
    Task<Basket> GetSingleBasketByUserIdAsync(Guid id);
    Task<Basket> GetSingleBasketWithDetailsByUserIdAsync(Guid id);
}
