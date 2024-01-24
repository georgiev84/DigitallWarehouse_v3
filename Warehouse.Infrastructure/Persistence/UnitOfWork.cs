using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Infrastructure.Persistence.Contexts;

namespace Warehouse.Infrastructure.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly WarehouseDbContext _dbContext;
    public IProductRepository Products { get; }
    public ISizeRepository Sizes { get; }
    public UnitOfWork(WarehouseDbContext dbContext, IProductRepository productRepository, ISizeRepository sizes)
    {
        _dbContext = dbContext;
        Products = productRepository;
        Sizes = sizes;
    }

    public int Save()
    {
        return _dbContext.SaveChanges();
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
