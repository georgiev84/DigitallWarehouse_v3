using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Products;
using Warehouse.Domain.Exceptions;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.EF.Persistence.Contexts;

namespace Warehouse.Persistence.EF.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsDetailsAsync()
    {
        try
        {
            var result = await _dbContext.Set<Product>()
                .Include(p => p.Brand)
                .Include(p => p.ProductGroups).ThenInclude(pg => pg.Group)
                .Include(p => p.ProductSizes).ThenInclude(ps => ps.Size)
                .Where(p => p.IsDeleted == false)
                .ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw new ProductNotFoundException("No products found in the database", ex);

        }
    }

    public async Task<Product> GetProductDetailsByIdAsync(Guid productId)
    {
        try
        {
            var result = await _dbContext.Set<Product>()
                .Include(p => p.Brand)
                .Include(p => p.ProductGroups).ThenInclude(pg => pg.Group)
                .Include(p => p.ProductSizes).ThenInclude(ps => ps.Size)
                .SingleAsync(p => p.Id == productId);

            return result;
        }
        catch (InvalidOperationException ex)
        {
            throw new ProductNotFoundException($"Product with ID {productId} not found.", ex);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}