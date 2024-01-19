using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.Requests;
using Warehouse.Api.Models.Responses;
using Warehouse.Application.Features.Queries.Product;

namespace Warehouse.Api.Controllers;

public class WarehouseController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetProducts(
         [FromQuery] ProductFilterRequest productFilter,
         [FromServices] ISender _mediator, 
         [FromServices] IMapper _mapper)
    {
        var query = _mapper.Map<ProductQuery>(productFilter);

        var products = await _mediator.Send(query);

        var mappedProducts = _mapper.Map<ProductResponse>(products);

        return Ok(mappedProducts);
    }
}
