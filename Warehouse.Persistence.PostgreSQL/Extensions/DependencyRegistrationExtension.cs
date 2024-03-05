using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Persistence.PostgreSQL.Persistence;
using Warehouse.Persistence.PostgreSQL.Persistence.Contexts;
using Warehouse.Persistence.PostgreSQL.Persistence.Repositories;

namespace Warehouse.Persistence.PostgreSQL.Extensions;

public static class DependencyRegistrationExtension
{
    public static IServiceCollection AddPersistenceEF(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        services.AddDbContext<WarehouseDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("WarehousePgsqlDbConnectionString"))
        );

        services.AddScoped<NpgsqlConnection>(provider =>
        {
            var connectionString = configuration.GetConnectionString("WarehousePgsqlDbConnectionString");
            return new NpgsqlConnection(connectionString);
        });

        services.AddScoped<IDbConnection>(provider =>
        {
            return provider.GetRequiredService<NpgsqlConnection>();
        });

        services.AddScoped<IDbTransaction>(provider =>
        {
            var connection = provider.GetRequiredService<NpgsqlConnection>();
            connection.Open();
            return connection.BeginTransaction();
        });

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