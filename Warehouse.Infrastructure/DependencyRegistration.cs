using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Infrastructure.Persistence;
using Warehouse.Infrastructure.Services;

namespace Warehouse.Infrastructure;

public static class DependencyRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClient<MockApiService>();
        services.AddScoped<IMockApiService, MockApiService>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}
