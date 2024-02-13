using Warehouse.Domain.Entities;
using Warehouse.Persistence.Abstractions.Interfaces;

namespace Warehouse.Application.Common.Interfaces.Persistence;

public interface IBasketRepository : IGenericRepository<Basket>
{
    Task<Basket> GetSingleBasketByUserIdAsync(Guid id);

    Task<Basket> GetSingleBasketWithDetailsByUserIdAsync(Guid id);
}