using MediatR;
using Microsoft.Extensions.Logging;
using Warehouse.Application.Extensions;
using Warehouse.Application.Models.Dto.ProductDtos;

namespace Warehouse.Application.Behavior;

public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ProductDto
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
            LoggingExtensions.LogSendRequest(_logger, typeof(TRequest).Name, DateTime.UtcNow);

            var result = await next();

            LoggingExtensions.LogCompleteRequest(_logger, typeof(TRequest).Name, DateTime.UtcNow);

            return result;
        }
        catch (Exception ex)
        {
            LoggingExtensions.LogErrorRequest(_logger, typeof(TRequest).Name, DateTime.UtcNow, ex.Message);
            throw;
        }
    }
}