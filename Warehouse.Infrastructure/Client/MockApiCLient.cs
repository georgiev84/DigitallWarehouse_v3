using Microsoft.Extensions.Logging;
using System.Text.Json;
using Warehouse.Application.Common.Interfaces;
using Warehouse.Domain.Entities;
using Warehouse.Persistence.EF.Extensions;

namespace Warehouse.Persistence.EF.Client;

public class MockApiCLient : IMockApiClient
{
    private readonly ILogger<MockApiCLient> _logger;
    private readonly HttpClient _httpClient;

    public MockApiCLient(ILogger<MockApiCLient> logger, HttpClient httpClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(string url)
    {
        MockApiClientLoggerExtensions.LogApiFetch(_logger);
        
        try
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                MockApiClientLoggerExtensions.LogApiResponse(_logger, content);
                return products;
            }
            else
            {
                MockApiClientLoggerExtensions.LogFailedFetchProducts(_logger);
                throw new HttpRequestException($"Failed to fetch products. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching products.", ex);
        }
    }
}

