using Warehouse.Domain.Entities;

namespace Warehouse.Application.Common.Interfaces.Persistence;

public interface IWarehouseRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
}
