using MediatR;
using Microsoft.Extensions.Logging;
using Warehouse.Domain.Responses;

namespace Warehouse.Application.Behavior;

public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse : ProductDomainModel
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Send request {@RequestName}, {@DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);

            var result = await next();

            _logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while handling {@RequestType}", typeof(TRequest).Name);
            throw;
        }
    }
}
