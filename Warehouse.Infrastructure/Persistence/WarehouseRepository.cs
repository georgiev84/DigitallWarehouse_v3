using Microsoft.Extensions.Options;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Configuration;

namespace Warehouse.Infrastructure.Persistence;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly IMockApiClient _externalApiService;
    private static IEnumerable<Product>? _products;
   // private readonly string apiUrl = "https://run.mocky.io/v3/97aa328f-6f5d-458a-9fa4-55fe58eaacc9";
    private readonly MockyClientConfiguration _mockyClientConfiguration;

    public WarehouseRepository(IMockApiClient externalApiService, IOptions<MockyClientConfiguration> options)
    {
        _externalApiService = externalApiService ?? throw new ArgumentNullException(nameof(externalApiService));
        _mockyClientConfiguration = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Get Products by calling external api service
    /// </summary>
    /// <returns>List of products</returns>
    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        if (_products == null)
        {
            await FetchProducts();
        }

        return _products ?? Enumerable.Empty<Product>();
    }


    /// <summary>
    /// Fetch data from External API and fill the _products variable to mock database behavior
    /// </summary>
    /// <returns></returns>
    private async Task FetchProducts() 
    {
        var apiUrl = $"{_mockyClientConfiguration.BaseUrl}{_mockyClientConfiguration.ProductUrl}";
        _products = await _externalApiService.GetProductsAsync(apiUrl);
    }

}
