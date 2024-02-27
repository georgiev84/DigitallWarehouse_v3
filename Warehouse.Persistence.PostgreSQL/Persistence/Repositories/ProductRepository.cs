using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Products;
using Warehouse.Domain.Exceptions;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.PostgreSQL.Configuration.Contstants;
using Warehouse.Persistence.PostgreSQL.Persistence.Contexts;
using static Dapper.SqlMapper;

namespace Warehouse.Persistence.PostgreSQL.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(WarehouseDbContext dbContext, IDbConnection dbConnection) : base(dbContext, dbConnection)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsDetailsAsync()
    {
        try
        {

            var result = await _dbConnection.QueryAsync<Product, Brand, ProductGroup, ProductSize, Size, Group, Product>(
                DapperConstants.GetProductsDetailsQuery,
                (product, brand, productGroup, productSize, size, group) =>
                {


                    product.Brand = brand;
                    product.ProductSizes = product.ProductSizes ?? new List<ProductSize>();
                    productSize.Size = size;
                    product.ProductSizes.Add(productSize);
                    product.ProductGroups = product.ProductGroups ?? new List<ProductGroup>();
                    productGroup.Group = group;
                    product.ProductGroups.Add(productGroup);
                    return product;
                },
                splitOn: "Id, ProductId, ProductId, Id, Id"
            );
            //var distinctResult = result.Distinct();
           
            // Dictionary to store product sizes by product ID
            var productSizesDict = new Dictionary<Guid, List<ProductSize>>();
            // Dictionary to store product groups by product ID
            var productGroupsDict = new Dictionary<Guid, List<ProductGroup>>();

            // Iterate over the result to populate dictionaries
            foreach (var product in result)
            {
                if (!productSizesDict.ContainsKey(product.Id))
                {
                    productSizesDict[product.Id] = new List<ProductSize>();
                }
                productSizesDict[product.Id].AddRange(product.ProductSizes);

                if (!productGroupsDict.ContainsKey(product.Id))
                {
                    productGroupsDict[product.Id] = new List<ProductGroup>();
                }
                productGroupsDict[product.Id].AddRange(product.ProductGroups);
            }

            foreach (var product in result)
            {
                if (productSizesDict.TryGetValue(product.Id, out var sizes))
                {
                    product.ProductSizes = sizes.Distinct().ToList();
                }
                if (productGroupsDict.TryGetValue(product.Id, out var groups))
                {
                    product.ProductGroups = groups
                        .GroupBy(pg => new { pg.ProductId, pg.GroupId }) // Group by both ProductId and GroupId
                        .Select(g => g.First()) // Select the first item of each group
                        .ToList();
                }
            }

            return result.DistinctBy(p => p.Id); 
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

            string query = """
                SELECT p.*, b.*, pg.*, ps.*, s.*, g.*
                FROM "Products" p
                LEFT JOIN "Brands" b ON p."BrandId" = b."Id"
                LEFT JOIN "ProductGroups" pg ON p."Id" = pg."ProductId"
                LEFT JOIN "Groups" g ON pg."GroupId" = g."Id"
                LEFT JOIN "ProductSizes" ps ON p."Id" = ps."ProductId"
                LEFT JOIN "Sizes" s ON ps."SizeId" = s."Id"
                WHERE p."IsDeleted" = false
                AND p."Id" = @ProductId
                """;

            var result1 = await _dbConnection.QueryAsync<Product, Brand, ProductGroup, ProductSize, Size, Group, Product>(
                query,
                (product, brand, productGroup, productSize, size, group) =>
                {
                    product.Brand = brand;
                    product.ProductSizes = product.ProductSizes ?? new List<ProductSize>();
                    productSize.Size = size;
                    product.ProductSizes.Add(productSize);
                    product.ProductGroups = product.ProductGroups ?? new List<ProductGroup>();
                    productGroup.Group = group;
                    product.ProductGroups.Add(productGroup);
                    return product;
                },
                new { ProductId = productId },
                splitOn: "Id, ProductId, ProductId, Id, Id"
            );


            return result;
        }
        catch (InvalidOperationException ex)
        {
            throw new ProductNotFoundException($"Product with ID {productId} not found.", ex);
        }
        catch
        {
            throw;
        }
    }

    public override void Update(Product entity)
    {
        string sql = @$"UPDATE Products SET Title = @Title, Description = @Description, Price = @Price, IsDeleted = @IsDeleted WHERE Id = @Id";

        _dbConnection.ExecuteAsync(sql, entity);
    }


}