using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Models.Requests.Product;
using Warehouse.Application.Features.Commands.Product.ProductCreate;
using Warehouse.Application.Features.Commands.Product.Delete;
using Warehouse.Application.Features.Commands.Product.Update;
using Warehouse.Application.Features.Queries.Product.ProductList;
using Warehouse.Api.Models.Responses.ProductResponses;

namespace Warehouse.Api.Controllers;

public class ProductsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetProducts(
         [FromQuery] ProductFilterRequest productFilter,
         [FromServices] ISender _mediator,
         [FromServices] IMapper _mapper)
    {
        var query = _mapper.Map<ProductListQuery>(productFilter);

        var products = await _mediator.Send(query);

        var mappedProducts = _mapper.Map<ProductDetailedResponse>(products);

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
        var command = _mapper.Map<ProductCreateCommand>(productCreateRequest);

        var result = await _mediator.Send(command);

        var mappedResult = _mapper.Map<ProductCreateResponse>(result);

        return CreatedAtAction(nameof(CreateProduct), new { id = result.Id }, mappedResult);
    }

    [HttpPut("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProduct(
        Guid productId,
        [FromBody] ProductUpdateRequest productUpdateRequest,
        [FromServices] ISender _mediator,
         [FromServices] IMapper _mapper)
    {
        productUpdateRequest.Id = productId;
        var command = _mapper.Map<ProductUpdateCommand>(productUpdateRequest);

        var result = await _mediator.Send(command);

        var mappedResult = _mapper.Map<ProductUpdateResponse>(result);

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
        var command = new ProductDeleteCommand(productId);

        await _mediator.Send(command);

        return NoContent();
    }
}
