using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.Requests.Basket;
using Warehouse.Api.Models.Requests.BasketLine;
using Warehouse.Api.Models.Responses.BasketResponses;
using Warehouse.Application.Features.Commands.Basket.BasketUpdate;
using Warehouse.Application.Features.Commands.BasketLine.BasketLineBulkDelete;
using Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;
using Warehouse.Application.Features.Commands.BasketLine.BasketLineDelete;
using Warehouse.Application.Features.Queries.Basket.BasketSingleQuery;

namespace Warehouse.Api.Controllers;

public class BasketController : BaseController
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetSingleBasket(
        [FromRoute] Guid userId,
        [FromServices] ISender _mediator,
        [FromServices] IMapper _mapper)
    {
        var query = new BasketSingleQuery { UserId = userId };

        var basket = await _mediator.Send(query);

        var mappedProducts = _mapper.Map<BasketResponse>(basket);

        return Ok(mappedProducts);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddBasketLine(
        [FromBody] BasketLineCreateRequest basketLineCreateRequest,
        [FromServices] ISender _mediator,
        [FromServices] IMapper _mapper)
    {
        var command = _mapper.Map<BasketLineCreateCommand>(basketLineCreateRequest);

        var result = await _mediator.Send(command);

        var mappedResult = _mapper.Map<BasketLineResponse>(result);

        return CreatedAtAction(nameof(AddBasketLine), new { id = result.BasketId }, mappedResult);
    }

    [HttpDelete("basketLines/{basketLineId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteSingleBasketLine(
    [FromRoute] Guid basketLineId,
    [FromServices] ISender _mediator)
    {
        var command = new BasketLineDeleteCommand(basketLineId);

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteBulkBasket(
        [FromRoute] Guid userId,
        [FromServices] ISender _mediator)
    {
        var command = new BasketLineBulkDeleteCommand(userId);

        await _mediator.Send(command);

        return NoContent();
    }

    //[HttpPut("{basketId}/{basketLineId}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> UpdateBasketLine(
    //Guid basketId,
    //Guid basketLineId,
    //[FromBody] BasketLineUpdateRequest basketLineUpdateRequest,
    //[FromServices] ISender _mediator,
    //[FromServices] IMapper _mapper)
    //{
    //    basketLineUpdateRequest.Id = basketId;
    //    var command = _mapper.Map<BasketLineUpdateCommand>(basketLineUpdateRequest);

    //    var result = await _mediator.Send(command);

    //    var mappedResult = _mapper.Map<BasketLineResponse>(result);

    //    return Ok(mappedResult);
    //}


    [HttpPut("{basketId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBasket(
        Guid basketId,
        [FromBody] BasketUpdateRequest basketUpdateRequest,
        [FromServices] ISender _mediator,
        [FromServices] IMapper _mapper)
    {
        basketUpdateRequest.Id = basketId;
        var command = _mapper.Map<BasketUpdateCommand>(basketUpdateRequest);

        var result = await _mediator.Send(command);

        var mappedResult = _mapper.Map<BasketResponse>(result);

        return Ok(mappedResult);
    }
}
