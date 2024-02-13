using Warehouse.Domain.Entities;

namespace Warehouse.Application.Common.Interfaces;

public interface IMockApiClient
{
    Task<IEnumerable<Product>> GetProductsAsync(string url);
}