using Microsoft.Extensions.Logging;

namespace Warehouse.Persistence.EF.Extensions;

public static partial class MockApiClientLoggerExtensions
{
    [LoggerMessage(EventId = 10, Level = LogLevel.Error, Message = "Failed to fetch products.")]
    public static partial void LogFailedFetchProducts(this ILogger logger);

    [LoggerMessage(EventId = 11, Level = LogLevel.Debug, Message = "Api response: {Response}")]
    public static partial void LogApiResponse(this ILogger logger, string Response);

    [LoggerMessage(EventId = 12, Level = LogLevel.Debug, Message = "Fetching information from mock api...")]
    public static partial void LogApiFetch(this ILogger logger);
}