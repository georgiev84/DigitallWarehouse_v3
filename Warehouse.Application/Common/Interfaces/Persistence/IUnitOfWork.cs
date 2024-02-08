﻿namespace Warehouse.Application.Common.Interfaces.Persistence;
public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ISizeRepository Sizes { get; }
    IOrderRepository Orders { get; }
    IBasketRepository Baskets { get; }
    IBasketLineRepository BasketLines { get; }
    int Save();
}
