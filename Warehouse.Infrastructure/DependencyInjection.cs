using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Persistence;
using Warehouse.Infrastructure.Persistence;
using Warehouse.Infrastructure.Services;

namespace Warehouse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClient<ExternalApiService>();
        services.AddScoped<IExternalApiService, ExternalApiService>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}
