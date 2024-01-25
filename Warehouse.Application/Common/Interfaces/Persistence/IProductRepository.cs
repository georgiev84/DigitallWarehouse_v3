using Warehouse.Domain.Entities;
using Warehouse.Domain.Models;


namespace Warehouse.Application.Common.Interfaces.Persistence;
public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<ProductDetailsDto>> GetProductsDetailsAsync();
}
