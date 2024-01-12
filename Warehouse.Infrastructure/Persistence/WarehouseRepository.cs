using Microsoft.Extensions.Logging;
using Serilog;
using System.Text.Json;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Application.Common.Persistence;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Persistence;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly HttpClient _httpClient;
    private readonly IExternalApiService _externalApiService;
    private static IEnumerable<Product>? _products;
    private readonly string apiUrl= "https://run.mocky.io/v3/97aa328f-6f5d-458a-9fa4-55fe58eaacc9";

    public WarehouseRepository(HttpClient httpClient, IExternalApiService externalApiService)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _externalApiService = externalApiService ?? throw new ArgumentNullException(nameof(externalApiService)); ; ;

        // Fetch data from api and assign it to _products variable
        if (_products == null)
        {
            FetchProducts().GetAwaiter().GetResult();
        }
       
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
    private async Task FetchProducts() => _products = await _externalApiService.GetProductsAsync(apiUrl);

}
