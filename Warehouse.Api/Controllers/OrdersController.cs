using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.Requests.Orders;
using Warehouse.Api.Models.Responses;
using Warehouse.Application.Features.Commands.Order.OrderCreate;
using Warehouse.Application.Features.Queries.Order.OrderGetSingle;
using Warehouse.Application.Features.Queries.Order.OrderGetAll;

namespace Warehouse.Api.Controllers;

public class OrdersController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetOrders(
         [FromQuery] OrderRequest orderRequest,
         [FromServices] ISender _mediator,
         [FromServices] IMapper _mapper)
    {
        var query = _mapper.Map<OrderGetAllQuery>(orderRequest);

        var orders = await _mediator.Send(query);

        var mappedProducts = _mapper.Map<IEnumerable<OrderResponse>>(orders);

        return Ok(mappedProducts);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetSingleOrder(
     [FromRoute] Guid orderId,
     [FromServices] ISender _mediator,
     [FromServices] IMapper _mapper)
    {

        var query = new OrderGetSingleQuery { OrderId = orderId };

        var order = await _mediator.Send(query);

        var mappedProducts = _mapper.Map<OrderResponse>(order);

        return Ok(mappedProducts);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateOrder(
    [FromBody] OrderCreateRequest orderCreateRequest,
    [FromServices] ISender _mediator,
     [FromServices] IMapper _mapper)
    {
        var command = _mapper.Map<CreateOrderCommand>(orderCreateRequest);

        var result = await _mediator.Send(command);

        var mappedResult = _mapper.Map<CreateOrderResponse>(result);

        return Created();
        //return CreatedAtAction(nameof(CreateOrder), new { id = result.Id }, mappedResult);
    }
}
