using MediatR;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Commands.Product;
public record CreateProductCommand : IRequest<CreateProductDetailsDto>
{
    public Guid BrandId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public List<Guid> GroupIds { get; set; }
    public List<SizeInformationDto> SizeInformation { get; set; }
}
