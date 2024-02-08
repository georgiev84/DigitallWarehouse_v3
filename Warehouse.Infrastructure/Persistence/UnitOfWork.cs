﻿using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Infrastructure.Persistence.Contexts;

namespace Warehouse.Infrastructure.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly WarehouseDbContext _dbContext;
    public IProductRepository Products { get; }
    public ISizeRepository Sizes { get; }
    public IOrderRepository Orders { get; }
    public IBasketRepository Baskets { get; }
    public IBasketLineRepository BasketLines { get; }
    public UnitOfWork(
        WarehouseDbContext dbContext,
        IProductRepository productRepository,
        ISizeRepository sizes,
        IOrderRepository orders,
        IBasketRepository baskets,
        IBasketLineRepository basketLines)
    {
        _dbContext = dbContext;
        Products = productRepository;
        Sizes = sizes;
        Orders = orders;
        Baskets = baskets;
        BasketLines = basketLines;
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
