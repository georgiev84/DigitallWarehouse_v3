using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Infrastructure.Client;
using Warehouse.Infrastructure.Configuration;
using Warehouse.Infrastructure.Persistence;
using Warehouse.Infrastructure.Persistence.Contexts;
using Warehouse.Infrastructure.Persistence.Repositories;
using Warehouse.Infrastructure.Services;

namespace Warehouse.Infrastructure.Extensions;

public static class DependencyRegistrationExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        services.AddDbContext<WarehouseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("WarehouseDbConnection")));
        //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISizeRepository, SizeRepository>();
        services.Configure<MockyClientConfiguration>(configuration.GetSection("MockyClient"));
        services.AddHttpClient<MockApiCLient>();
        services.AddScoped<IMockApiClient, MockApiCLient>();
        services.AddScoped<IProductService, ProductService>();
        
        return services;
    }
}
