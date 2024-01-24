using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Persistence.Contexts;

namespace Warehouse.Infrastructure.Persistence.Repositories;
public class SizeRepository : GenericRepository<Size>, ISizeRepository
{
    public SizeRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<string>> GetSizeNamesAsync()
    {
        return await _dbContext.Sizes.Select(s=>s.Name).ToListAsync();
    }
}

