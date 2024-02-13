using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;
using System.Net;
using Warehouse.Api.Models.Errors;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Api.Filters;

public class ErrorHandlingFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var exceptionType = exception.GetType();

        var error = new Error
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Message = exception.Message
        };

        if (exceptionType == typeof(DBConcurrencyException))
        {
            error.Message = exception.Message;
            error.StatusCode = HttpStatusCode.Conflict;
        }

        if (exceptionType == typeof(ProductNotFoundException))
        {
            error.Message = exception.Message;
            error.StatusCode = HttpStatusCode.NotFound;
        }

        if (exceptionType == typeof(ProductCreationException))
        {
            error.Message = exception.Message;
            error.StatusCode = HttpStatusCode.Conflict;
        }

        context.Result = new JsonResult(error);
        context.ExceptionHandled = true;
    }
}