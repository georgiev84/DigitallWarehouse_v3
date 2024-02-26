using Microsoft.EntityFrameworkCore;
using Warehouse.Persistence.Abstractions.Interfaces;

namespace Warehouse.Persistence.Abstractions;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly DbContext _dbContext;

    protected GenericRepository(DbContext context)
    {
        _dbContext = context;
    }

    public async Task<TEntity> GetById(Guid id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task Add(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
    }

    public void Delete(TEntity entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbContext.Set<TEntity>().Attach(entity);
        }

        _dbContext.Set<TEntity>().Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }
}