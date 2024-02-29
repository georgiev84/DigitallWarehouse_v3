using System.Data;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Persistence.PostgreSQL.Persistence.Contexts;

namespace Warehouse.Persistence.PostgreSQL.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbTransaction _dbTransaction;
    private readonly WarehouseDbContext _dbContext;
    public IProductRepository Products { get; }
    public ISizeRepository Sizes { get; }
    public IOrderRepository Orders { get; }
    public IBasketRepository Baskets { get; }
    public IBasketLineRepository BasketLines { get; }
    public IProductSizeRepository ProductSizes { get; }

    public UnitOfWork(
        WarehouseDbContext dbContext,
        IProductRepository productRepository,
        ISizeRepository sizes,
        IOrderRepository orders,
        IBasketRepository baskets,
        IBasketLineRepository basketLines,
        IProductSizeRepository productSizes,
        IDbTransaction dbTransaction)
    {
        _dbContext = dbContext;
        Products = productRepository;
        Sizes = sizes;
        Orders = orders;
        Baskets = baskets;
        BasketLines = basketLines;
        ProductSizes = productSizes;
        _dbTransaction = dbTransaction;
    }

    public async Task<int> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Commit()
    {
        _dbTransaction.Commit();
    }

    public void Rollback()
    {
        _dbTransaction.Rollback();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }
}