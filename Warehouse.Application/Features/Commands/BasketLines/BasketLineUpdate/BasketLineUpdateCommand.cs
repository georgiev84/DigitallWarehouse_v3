using MediatR;
using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Application.Features.Commands.BasketLines.BasketLineUpdate;
public record BasketLineUpdateCommand(
    Guid BasketLineId,
    Guid SizeId,
    int Quantity) : IRequest<BasketLineUpdateDto>;