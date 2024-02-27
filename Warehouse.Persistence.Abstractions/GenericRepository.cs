using Dapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using Warehouse.Persistence.Abstractions.Interfaces;

namespace Warehouse.Persistence.Abstractions;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly DbContext _dbContext;
    protected readonly IDbConnection _dbConnection;

    protected GenericRepository(DbContext context, IDbConnection dbConnection)
    {
        _dbContext = context;
        _dbConnection = dbConnection;
    }

    public async Task<TEntity> GetById(Guid id)
    {
        //return await _dbContext.Set<TEntity>().FindAsync(id);
        var tableName = GetTableName<TEntity>();
        var parameters = new DynamicParameters();
        parameters.Add("id", id);
        parameters.Add(tableName, tableName, DbType.AnsiString);
        string sql = $@"SELECT * FROM ""{tableName}"" WHERE ""Id"" = @Id";

        return await _dbConnection.QueryFirstOrDefaultAsync<TEntity>(sql, parameters);
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

    public virtual void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }
    public string GetTableName<TEntity>() where TEntity : class
    {
        var tableAttribute = typeof(TEntity).GetCustomAttribute<TableAttribute>();
        if (tableAttribute != null && !string.IsNullOrWhiteSpace(tableAttribute.Name))
        {
            return tableAttribute.Name;
        }

        return typeof(TEntity).Name + "s"; // Default: pluralize entity name
    }
}