using MediatR;
using Warehouse.Application.Models.Dto;

namespace Warehouse.Application.Features.Commands.Basket.BasketUpdate;
public record BasketUpdateCommand(
    Guid BasketId,
    Guid UserId,
    List<BasketLineDto> BasketLines) : IRequest<BasketUpdateDto>;
