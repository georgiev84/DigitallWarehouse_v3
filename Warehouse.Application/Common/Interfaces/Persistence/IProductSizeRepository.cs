using Warehouse.Domain.Entities.Products;
using Warehouse.Persistence.Abstractions.Interfaces;

namespace Warehouse.Application.Common.Interfaces.Persistence;

public interface IProductSizeRepository : IGenericRepository<ProductSize>
{
    Task<ProductSize> GetByCompositeKey(Guid productId, Guid sizeId);
}