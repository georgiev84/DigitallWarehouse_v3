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

        switch (exceptionType.Name)
        {
            case nameof(DBConcurrencyException):
            case nameof(ProductCreationException):
            case nameof(BasketLineExistException):
                error.StatusCode = HttpStatusCode.Conflict;
                break;
            case nameof(ProductNotFoundException):
            case nameof(BasketNotFoundException):
                error.StatusCode = HttpStatusCode.NotFound;
                break;
        }

        context.Result = new JsonResult(error);
        context.ExceptionHandled = true;
    }
}