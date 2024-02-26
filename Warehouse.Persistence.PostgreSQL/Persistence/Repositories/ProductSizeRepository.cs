using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Products;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Exceptions.ProductExceptions;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.PostgreSQL.Persistence.Contexts;

namespace Warehouse.Persistence.PostgreSQL.Persistence.Repositories;

public class ProductSizeRepository : GenericRepository<ProductSize>, IProductSizeRepository
{
    public ProductSizeRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<ProductSize> GetByCompositeKey(Guid productId, Guid sizeId)
    {
        try
        {
            return await _dbContext.Set<ProductSize>().FindAsync(productId, sizeId);
        }
        catch (InvalidOperationException ex)
        {
            throw new ProductSizeNotFoundException($"ProductSize with ProductId {productId} and SizeId {sizeId} not found.", ex);
        }
        catch
        {
            throw;
        }
    }
}