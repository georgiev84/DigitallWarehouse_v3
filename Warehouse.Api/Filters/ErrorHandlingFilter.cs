using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Warehouse.Domain.Errors;

namespace Warehouse.Api.Filters;

public class ErrorHandlingFilter : IExceptionFilter
{
    ILogger<ErrorHandlingFilter> _logger;

    public ErrorHandlingFilter(ILogger<ErrorHandlingFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var exceptionType = exception.GetType();

        var error = new Error
        {
            StatusCode = 500,
            Message = exception.Message
        };

        if (exceptionType == typeof(ArgumentNullException))
        {
            error.Message = exception.Message;
            error.StatusCode = 400;
        }
        else if (exceptionType == typeof(InvalidOperationException))
        {
            error.Message = exception.Message;
            error.StatusCode = 409;
        }

        _logger.LogError("Exception occured in {Method} with Message: {Exception}", context.ActionDescriptor?.DisplayName, error.Message);

        context.Result = new JsonResult(error);
    }
}
