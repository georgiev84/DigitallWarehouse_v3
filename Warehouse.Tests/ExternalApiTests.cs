using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Services;
using Xunit;


namespace Warehouse.Tests;

public class ExternalApiTests
{
    [Fact]
    public async Task GetProductsAsync_UnsuccessfulResponse_ThrowsHttpRequestException()
    {
        // Arrange
        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        // Set up the SendAsync method behavior.
        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

        // create the HttpClient with the mocked HttpMessageHandler
        var httpClient = new HttpClient(httpMessageHandlerMock.Object);

        var logger = new Mock<ILogger<ExternalApiService>>();

        var apiService = new ExternalApiService(logger.Object, httpClient);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => apiService.GetProductsAsync("https://example.com/dummyUrl"));

        // Assert the inner exception type and message
        Assert.IsType<HttpRequestException>(exception.InnerException);
        Assert.Contains("Failed to fetch products. Status code: BadRequest", exception.InnerException.Message);
    }

    [Fact]
    public async Task GetProductsAsync_SuccessfulResponse_ReturnsProducts()
    {
        // Arrange
        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        // Set up the SendAsync method behavior.
        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{\"Title\":\"Product1\",\"Price\":19.99,\"Sizes\":[\"S\",\"M\",\"L\"],\"Description\":\"Description for Product1\"}]"),
            });


        var httpClient = new HttpClient(httpMessageHandlerMock.Object);

        var logger = new Mock<ILogger<ExternalApiService>>();

        var apiService = new ExternalApiService(logger.Object, httpClient);

        // Act
        var products = await apiService.GetProductsAsync("https://example.com/dummyUrl");

        // Assert
        Assert.NotNull(products);
        Assert.Single(products); 
        Assert.Equal("Product1", products.First().Title);
        Assert.Equal(19.99m, products.First().Price);
    }

}
