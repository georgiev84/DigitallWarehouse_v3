using Microsoft.EntityFrameworkCore;
using System.Data;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Products;
using Warehouse.Domain.Exceptions;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.PostgreSQL.Configuration.Contstants;
using Warehouse.Persistence.PostgreSQL.Configuration.Contstants.DapperProductConstants;
using Warehouse.Persistence.PostgreSQL.Persistence.Contexts;
using static Dapper.SqlMapper;

namespace Warehouse.Persistence.PostgreSQL.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly IDbTransaction _dbTransaction;

    public ProductRepository(WarehouseDbContext dbContext, IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbContext, dbConnection)
    {
        _dbTransaction = dbTransaction;
    }

    public async Task<IEnumerable<Product>> GetProductsDetailsAsync()
    {
        try
        {
            var result = await _dbConnection.QueryAsync<Product, Brand, ProductGroup, ProductSize, Size, Group, Product>(
                DapperProductReadConst.GetProductsDetailsQuery,
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
                splitOn: $"{nameof(Brand.Id)}, {nameof(ProductGroup.ProductId)}, {nameof(ProductSize.ProductId)}, {nameof(Size.Id)}, {nameof(Group.Id)}"
            );

            PopulateProductSizesAndGroups(result);

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
            var result = await _dbConnection.QueryAsync<Product, Brand, ProductGroup, ProductSize, Size, Group, Product>(
                DapperProductReadConst.GetProductsDetailsSingleQuery,
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
                splitOn: $"{nameof(Brand.Id)}, {nameof(ProductGroup.ProductId)}, {nameof(ProductSize.ProductId)}, {nameof(Size.Id)}, {nameof(Group.Id)}"
            );

            PopulateProductSizesAndGroups(result);

            return result.FirstOrDefault();
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

    public override async Task Update(Product entity)
    {
        try
        {
            await _dbConnection.ExecuteAsync(DapperProductMutateConst.UpdateProductQuery, entity, _dbTransaction);
            await _dbConnection.ExecuteAsync(DapperProductMutateConst.DeleteProductSizesQuery, new { ProductId = entity.Id }, _dbTransaction);
            await _dbConnection.ExecuteAsync(DapperProductMutateConst.InsertProductSizesQuery, entity.ProductSizes.Select(ps => new { ProductId = entity.Id, ps.SizeId, ps.QuantityInStock }), _dbTransaction);
            await _dbConnection.ExecuteAsync(DapperProductMutateConst.DeleteProductGroupsQuery, new { ProductId = entity.Id }, _dbTransaction);
            await _dbConnection.ExecuteAsync(DapperProductMutateConst.InsertProductGroupsQuery, entity.ProductGroups.Select(pg => new { ProductId = entity.Id, pg.GroupId }), _dbTransaction);
        }
        catch
        {
            throw;
        }
    }

    public override async Task Add(Product entity)
    {
        try
        {
            entity.Id = Guid.NewGuid();
            await _dbConnection.ExecuteAsync(DapperProductMutateConst.InsertProductQuery, entity);
            await _dbConnection.ExecuteAsync(DapperProductMutateConst.InsertProductSizesQuery, entity.ProductSizes.Select(ps => new { ProductId = entity.Id, ps.SizeId, ps.QuantityInStock }));
            await _dbConnection.ExecuteAsync(DapperProductMutateConst.InsertProductGroupsQuery, entity.ProductGroups.Select(pg => new { ProductId = entity.Id, pg.GroupId }));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public override void Delete(Product entity)
    {
        try
        {
            _dbConnection.Execute(DapperProductMutateConst.SetDeleteProductQuery, new { IsDeleted = true, Id = entity.Id }, _dbTransaction);
        }
        catch
        {
            throw;
        }
    }

    private void PopulateProductSizesAndGroups(IEnumerable<Product> products)
    {
        var productSizesDict = new Dictionary<Guid, List<ProductSize>>();
        var productGroupsDict = new Dictionary<Guid, List<ProductGroup>>();

        foreach (var product in products)
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

        foreach (var product in products)
        {
            if (productSizesDict.TryGetValue(product.Id, out var sizes))
            {
                product.ProductSizes = sizes
                  .GroupBy(ps => ps.SizeId)
                  .Select(group => group.First())
                  .ToList();
            }
            if (productGroupsDict.TryGetValue(product.Id, out var groups))
            {
                product.ProductGroups = groups
                    .GroupBy(pg => new { pg.ProductId, pg.GroupId })
                    .Select(g => g.First())
                    .ToList();
            }
        }
    }
}