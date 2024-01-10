using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Warehouse.Domain.Errors;

namespace Warehouse.Api.Filters;

public class ErrorHandlingFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var error = new Error
        {
            StatusCode = 500,
            Message = context.Exception.Message
        };

        context.Result = new JsonResult(error) { StatusCode = 500 };
    }
}
