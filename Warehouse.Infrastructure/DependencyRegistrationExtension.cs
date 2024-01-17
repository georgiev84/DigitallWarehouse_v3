using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Infrastructure.Client;
using Warehouse.Infrastructure.Configuration;
using Warehouse.Infrastructure.Persistence;
using Warehouse.Infrastructure.Services;

namespace Warehouse.Infrastructure;

public static class DependencyRegistrationExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        services.Configure<MockyClientConfiguration>(configuration.GetSection("MockyClient"));
        services.AddHttpClient<MockApiCLient>();
        services.AddScoped<IMockApiClient, MockApiCLient>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}
