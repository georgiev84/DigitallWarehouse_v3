using Microsoft.Data.SqlClient;
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
            options => options.UseNpgsql(configuration.GetConnectionString("WerehousePgsqlDbConnectionString"))
        );

        services.AddScoped<IDbConnection>(provider =>
        {
            var connectionString = configuration.GetConnectionString("WerehousePgsqlDbConnectionString");
            return new NpgsqlConnection(connectionString);
        });

        services.AddScoped((s) => new NpgsqlConnection(configuration.GetConnectionString("WerehousePgsqlDbConnectionString")));
        services.AddScoped<IDbTransaction>(s =>
        {
            var conn = s.GetRequiredService<NpgsqlConnection>();
            conn.Open();
            return conn.BeginTransaction();
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