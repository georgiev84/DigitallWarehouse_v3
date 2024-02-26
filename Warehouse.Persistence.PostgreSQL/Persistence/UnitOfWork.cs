using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Persistence.PostgreSQL.Persistence.Contexts;

namespace Warehouse.Persistence.PostgreSQL.Persistence;

public class UnitOfWork : IUnitOfWork
{
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
        IProductSizeRepository productSizes)
    {
        _dbContext = dbContext;
        Products = productRepository;
        Sizes = sizes;
        Orders = orders;
        Baskets = baskets;
        BasketLines = basketLines;
        ProductSizes = productSizes;
    }

    public async Task<int> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync();
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