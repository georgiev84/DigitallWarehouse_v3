using Warehouse.Domain.Entities;

namespace Warehouse.Application.Common.Interfaces;

public interface IExternalApiService
{
    Task<IEnumerable<Product>> GetProductsAsync(string url);
}
