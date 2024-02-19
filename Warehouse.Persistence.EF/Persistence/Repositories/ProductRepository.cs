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
        var result = await _dbContext.Set<Product>()
            .Include(p => p.Brand)
            .Include(p => p.ProductGroups).ThenInclude(pg => pg.Group)
            .Include(p => p.ProductSizes).ThenInclude(ps => ps.Size)
            .Where(p => p.IsDeleted == false)
            .ToListAsync();

        return result;
    }

    public async Task<Product> GetProductDetailsByIdAsync(Guid productId)
    {
        var result = await _dbContext.Set<Product>()
        .Where(p => p.Id == productId)
        .Include(p => p.Brand)
        .Include(p => p.ProductGroups).ThenInclude(pg => pg.Group)
        .Include(p => p.ProductSizes).ThenInclude(ps => ps.Size)
        .SingleAsync();

        if (result is null)
        {
            throw new ProductNotFoundException($"Product with ID {productId} not found.");
        }

        return result;
    }
}