using Microsoft.Extensions.Logging;
using Serilog;
using System.Text.Json;
using Warehouse.Application.Common.Persistence;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Persistence;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly HttpClient _httpClient;
    private static IEnumerable<Product>? _products;
    private readonly ILogger<WarehouseRepository> _logger;
    private readonly string apiUrl= "https://run.mocky.io/v3/97aa328f-6f5d-458a-9fa4-55fe58eaacc9";

    public WarehouseRepository(HttpClient httpClient, ILogger<WarehouseRepository> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "httpClient cannot be null");
        _logger = logger;

        // Fetch data from api and assign it to _products variable
        if(_products == null)
        {
            MockDatabase().GetAwaiter().GetResult();
        }
       
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        if (_products == null)
        {
            await MockDatabase();
        }

        return _products ?? Enumerable.Empty<Product>();
    }

    /// <summary>
    /// Get Data from external API
    /// </summary>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    /// <exception cref="Exception"></exception>
    private async Task<IEnumerable<Product>> GetProductsFromExternalApiAsync()
    {
        _logger.LogInformation("Fetching information from External API...");
        try
        {
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                _logger.LogInformation("API response: " + content);

                return products;
            }
            else
            {
                _logger.LogInformation("Failed to fetch products.");
                throw new HttpRequestException($"Failed to fetch products. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching products.", ex);
        }
    }
    /// <summary>
    /// Fetch data from External API and fill the _products variable to mock database behavior
    /// </summary>
    /// <returns></returns>
    private async Task MockDatabase() => _products = await GetProductsFromExternalApiAsync();

}
