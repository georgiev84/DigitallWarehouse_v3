﻿using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities.Products;
using Warehouse.Persistence.Abstractions;
using Warehouse.Persistence.EF.Persistence.Contexts;

namespace Warehouse.Persistence.EF.Persistence.Repositories;

public class SizeRepository : GenericRepository<Size>, ISizeRepository
{
    public SizeRepository(WarehouseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<string>> GetSizeNamesAsync()
    {
        return await _dbContext.Set<Size>().Select(s => s.Name).ToListAsync();
    }
}