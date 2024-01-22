using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Infrastructure.Persistence.Contexts;

namespace Warehouse.Infrastructure.Persistence.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly WarehouseDbContext _dbContext;

    public Repository(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return _dbContext.Set<TEntity>();
    }

    public void Insert(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }
}
