using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models;
using Warehouse.Application.Models.Dto;
using Warehouse.Application.Queries.Warehouse;

namespace Warehouse.Api.Controllers;

public class WarehouseController : ApiController
{
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts([FromServices] ISender _mediator, [FromServices] IMapper _mapper, [FromQuery] ProductFilterModelDto productFilter)
    {
        var query = _mapper.Map<ProductQuery>(productFilter);

        var products = await _mediator.Send(query);

        return Ok(products);
    }
}
