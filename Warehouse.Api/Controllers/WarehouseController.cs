using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Dto;
using Warehouse.Api.Models;
using Warehouse.Application.Queries.Warehouse;

namespace Warehouse.Api.Controllers;

public class WarehouseController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetProducts(
         [FromQuery] ProductFilterRequest productFilter,
         [FromServices] ISender _mediator, 
         [FromServices] IMapper _mapper)
    {
        var query = _mapper.Map<ProductQuery>(productFilter);

        var products = await _mediator.Send(query);

        var mappedProducts = _mapper.Map<ProductDto>(products);

        return Ok(mappedProducts);
    }
}
