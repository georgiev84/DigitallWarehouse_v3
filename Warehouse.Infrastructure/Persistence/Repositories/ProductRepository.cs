using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Models;
using Warehouse.Infrastructure.Persistence.Contexts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Warehouse.Infrastructure.Persistence.Repositories;
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<ProductDetailsDto>> GetProductsDetailsAsync()
    {
        //var result = await _dbContext.Products
        //    .Include(p => p.ProductSizes)
        //        .ThenInclude(ps => ps.Size)
        //    .Include(p => p.Brand)
        //    .Include(p => p.ProductGroups)
        //        .ThenInclude(pg => pg.Group)
        //    .Select(p => new ProductDetailsDto
        //    {
        //        Id = p.Id,
        //        Title = p.Title,
        //        Description = p.Description,
        //        Price = p.Price,
        //        BrandName = p.Brand.Name,
        //        Groups = p.ProductGroups.Select(pg => pg.Group.Name).ToList(),
        //        Sizes = p.ProductSizes.Select(ps => ps.Size.Name).ToList()
        //    })
        //    .ToListAsync();


        var result = _dbContext.Products
            .Select(p => new ProductDetailsDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                BrandName = p.Brand.Name,
                Groups = p.ProductGroups.Select(pg => pg.Group.Name).ToList(),
                Sizes = p.ProductSizes.Select(ps => ps.Size.Name).ToList()
            });

        var sql = result.ToQueryString();
        Console.WriteLine(sql);



        return result;
    }
}
