using Dapper;
using System.Data;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Products;
using Warehouse.Domain.Exceptions.ProductExceptions;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.PostgreSQL.Configuration.Constants.ReadableQueries;
using Warehouse.Persistence.PostgreSQL.Configuration.Contstants;
using Warehouse.Persistence.PostgreSQL.Persistence.Contexts;

namespace Warehouse.Persistence.PostgreSQL.Persistence.Repositories;

public class ProductSizeRepository : GenericRepository<ProductSize>, IProductSizeRepository
{
    public ProductSizeRepository(WarehouseDbContext context, IDbConnection dbConnection) : base(context, dbConnection)
    {
    }

    public async Task<ProductSize> GetByCompositeKey(Guid productId, Guid sizeId)
    {
        try
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<ProductSize>(
                ReadableQuerySizeConst.GetProductSizeQuery,
                new { ProductId = productId, SizeId = sizeId }
            );
        }
        catch (Exception ex)
        {
            throw new ProductSizeNotFoundException($"ProductSize with ProductId {productId} and SizeId {sizeId} not found.", ex);
        }
    }
}