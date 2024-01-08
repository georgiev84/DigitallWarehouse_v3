using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Queries.Warehouse;
using Warehouse.Domain.Entities;

namespace Warehouse.Api.Controllers;

public class WarehouseController : ApiController
{
    ISender _mediator;

    public WarehouseController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery(Name = "MinPrice")] decimal? minPrice,
        [FromQuery(Name = "MaxPrice")] decimal? maxPrice,
        [FromQuery(Name = "Highlight")] string highlight = "",
        [FromQuery(Name = "Size")] string size = "")
    {
        try
        {
            var query = new ProductQuery
            {
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Highlight = highlight,
                Size = size
            };

            var products = await _mediator.Send(query);

            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
