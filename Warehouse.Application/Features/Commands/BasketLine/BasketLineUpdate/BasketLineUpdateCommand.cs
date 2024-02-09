using MediatR;
using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineUpdate;
public record BasketLineUpdateCommand(
    Guid BasketLineId,
    Guid SizeId,
    int Quantity) : IRequest<BasketLineUpdateDto>;