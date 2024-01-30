using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.Requests;
using Warehouse.Api.Models.Responses;
using Warehouse.Application.Features.Commands.Product;
using Warehouse.Application.Features.Queries.Product;

namespace Warehouse.Api.Controllers;

public class ProductsController : BaseController
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProduct(
        [FromBody] ProductCreateRequest productCreateRequest,
        [FromServices] ISender _mediator,
         [FromServices] IMapper _mapper)
    {
        var command = _mapper.Map<CreateProductCommand>(productCreateRequest);

        var result = await _mediator.Send(command);

        var mappedResult = _mapper.Map<CreateProductResponse>(result);

        return CreatedAtAction(nameof(CreateProduct), new { id = result.Id }, mappedResult);
    }

    [HttpPut("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProduct(
        [FromQuery] Guid productId,
        [FromBody] ProductUpdateRequest productUpdateRequest,
        [FromServices] ISender _mediator,
         [FromServices] IMapper _mapper)
    {
        productUpdateRequest.Id = productId;
        var command = _mapper.Map<UpdateProductCommand>(productUpdateRequest);

        var result = await _mediator.Send(command);

        var mappedResult = _mapper.Map<UpdateProductResponse>(result);

        return Ok(mappedResult);
    }

    [HttpDelete("{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProduct(
    [FromRoute] Guid productId,
    [FromServices] ISender _mediator)
    {
        var command = new DeleteProductCommand(productId);

        await _mediator.Send(command);

        return NoContent();
    }
}
