using Microsoft.Extensions.Logging;

namespace Warehouse.Application.Extensions;

public static partial class LoggingExtensions
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Information, Message = "Send request {RequestName}, {DateTimeUtc}")]
    public static partial void LogSendRequest(this ILogger logger, string RequestName, DateTime DateTimeUtc);

    [LoggerMessage(EventId = 0, Level = LogLevel.Information, Message = "Complete request {RequestName}, {DateTimeUtc}")]
    public static partial void LogCompleteRequest(this ILogger logger, string RequestName, DateTime DateTimeUtc);

    [LoggerMessage(EventId = 0, Level = LogLevel.Error, Message = "Error occurred while handling {RequestName}, {DateTimeUtc}, with Exception: {ExceptionMessage}")]
    public static partial void LogErrorRequest(this ILogger logger, string RequestName, DateTime DateTimeUtc, string ExceptionMessage);
}
