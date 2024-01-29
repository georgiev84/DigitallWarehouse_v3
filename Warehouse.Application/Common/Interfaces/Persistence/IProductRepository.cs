using Warehouse.Domain.Entities;


namespace Warehouse.Application.Common.Interfaces.Persistence;
public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsDetailsAsync();
    Task<Product> GetProductDetailsByIdAsync(Guid productId);
}
