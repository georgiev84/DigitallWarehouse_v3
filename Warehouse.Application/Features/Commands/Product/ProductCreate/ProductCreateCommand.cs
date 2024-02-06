using MediatR;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Commands.Product.ProductCreate;
public record ProductCreateCommand(
    Guid BrandId,
    string Title,
    string Description,
    decimal Price,
    List<Guid> GroupIds,
    List<SizeInformationDto> SizeInformation) : IRequest<OrderUpdateDto>;

