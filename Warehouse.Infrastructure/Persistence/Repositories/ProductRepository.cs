using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Persistence.Contexts;

namespace Warehouse.Infrastructure.Persistence.Repositories;
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsDetailsAsync()
    {
        var result = await _dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.ProductGroups).ThenInclude(pg => pg.Group)
            .Include(p => p.ProductSizes).ThenInclude(ps => ps.Size)
            .ToListAsync();

        return result;
    }

    public async Task<Product> GetProductDetailsByIdAsync(Guid productId)
    {
        var result = await _dbContext.Products
        .Where(p => p.Id == productId)
        .Include(p => p.Brand)
        .Include(p => p.ProductGroups).ThenInclude(pg => pg.Group)
        .Include(p => p.ProductSizes).ThenInclude(ps => ps.Size)
        .FirstOrDefaultAsync();

        return result;
    }

}
