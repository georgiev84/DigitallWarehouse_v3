using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.OrderResponses.Orders;
using Warehouse.Api.Models.Requests.Orders;
using Warehouse.Api.Models.Responses.OrderResponses;
using Warehouse.Application.Features.Commands.Order.OrderCreate;
using Warehouse.Application.Features.Commands.Order.OrderDelete;
using Warehouse.Application.Features.Commands.Order.OrderUpdate;
using Warehouse.Application.Features.Queries.Order.OrderGetAll;
using Warehouse.Application.Features.Queries.Order.OrderGetSingle;

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

        var mappedProducts = _mapper.Map<OrderDetailedResponse>(order);

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
        var command = _mapper.Map<OrderCreateCommand>(orderCreateRequest);

        var result = await _mediator.Send(command);

        var mappedResult = _mapper.Map<OrderCreateResponse>(result);

        return CreatedAtAction(nameof(CreateOrder), new { id = result.Id }, mappedResult);
    }

    [HttpPut("{orderId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateOrder(
    Guid orderId,
    [FromBody] OrderUpdateRequest orderUpdateRequest,
    [FromServices] ISender _mediator,
     [FromServices] IMapper _mapper)
    {
        orderUpdateRequest.Id = orderId;
        var command = _mapper.Map<OrderUpdateCommand>(orderUpdateRequest);

        var result = await _mediator.Send(command);

        var mappedResult = _mapper.Map<OrderResponse>(result);

        return Ok(mappedResult);
    }

    [HttpDelete("{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteOrder(
    [FromRoute] Guid orderId,
    [FromServices] ISender _mediator)
    {
        var command = new OrderDeleteCommand(orderId);

        await _mediator.Send(command);

        return NoContent();
    }
}
