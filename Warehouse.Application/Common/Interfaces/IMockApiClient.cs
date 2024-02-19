using Warehouse.Domain.Entities.Products;

namespace Warehouse.Application.Common.Interfaces;

public interface IMockApiClient
{
    Task<IEnumerable<Product>> GetProductsAsync(string url);
}