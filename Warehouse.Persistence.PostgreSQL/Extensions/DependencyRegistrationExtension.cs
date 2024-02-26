﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Persistence.EF.Persistence;
using Warehouse.Persistence.EF.Persistence.Contexts;
using Warehouse.Persistence.EF.Persistence.Repositories;

namespace Warehouse.Persistence.EF.Extensions;

public static class DependencyRegistrationExtension
{
    public static IServiceCollection AddPersistenceEF(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        services.AddDbContext<WarehouseDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("WerehousePgsqlDbConnectionString"))
        );
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISizeRepository, SizeRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IBasketLineRepository, BasketLineRepository>();
        services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
        return services;
    }
}