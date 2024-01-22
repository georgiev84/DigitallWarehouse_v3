using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Infrastructure.Persistence.Contexts;

namespace Warehouse.Infrastructure.Persistence;
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly WarehouseDbContext _dbContext;

    public UnitOfWork(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
