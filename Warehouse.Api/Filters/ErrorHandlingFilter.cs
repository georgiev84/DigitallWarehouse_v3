using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;
using System.Net;
using Warehouse.Api.Errors;

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

        if (exceptionType == typeof(AutoMapperMappingException))
        {
            error.Message = "Error mapping objects";
            error.StatusCode = HttpStatusCode.BadRequest;
        }
        else if (exceptionType == typeof(ArgumentNullException))
        {
            error.Message = exception.Message;
            error.StatusCode = HttpStatusCode.BadRequest;
        }
        else if (exceptionType == typeof(InvalidOperationException))
        {

            context.Result = new NotFoundResult();
            error.StatusCode = HttpStatusCode.InternalServerError;
            error.Message = "Inwvalid Operation";
        }
        else if (exceptionType == typeof(DBConcurrencyException))
        {
            error.Message = exception.Message;
            error.StatusCode = HttpStatusCode.Conflict;
        }

        context.Result = new JsonResult(error);
        context.ExceptionHandled = true;
    }
}
