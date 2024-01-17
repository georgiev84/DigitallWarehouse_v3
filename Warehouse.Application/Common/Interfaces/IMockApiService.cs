using Warehouse.Domain.Entities;

namespace Warehouse.Application.Common.Interfaces;

public interface IMockApiService
{
    Task<IEnumerable<Product>> GetProductsAsync(string url);
}
