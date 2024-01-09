using Warehouse.Domain.Entities;

namespace Warehouse.Application.Common.Persistence;

public interface IWarehouseRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
}
