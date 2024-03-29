﻿using Warehouse.Domain.Entities.Products;
using Warehouse.Persistence.Abstractions.Interfaces;

namespace Warehouse.Application.Common.Interfaces.Persistence;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsDetailsAsync();

    Task<Product> GetProductDetailsByIdAsync(Guid productId);
}