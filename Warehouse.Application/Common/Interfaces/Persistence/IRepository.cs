using System.Linq.Expressions;

namespace Warehouse.Application.Common.Interfaces.Persistence;
public interface IRepository<TEntity>
{
    void Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    IQueryable<TEntity> GetQueryable();
}
