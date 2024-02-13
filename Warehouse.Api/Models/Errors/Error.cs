using System.Net;

namespace Warehouse.Api.Models.Errors;

public class Error
{
    public HttpStatusCode StatusCode { get; set; }
    public string? Message { get; set; }
}