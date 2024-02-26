using MediatR;
using Warehouse.Application.Models.Dto.BasketDtos;

namespace Warehouse.Application.Features.Commands.BasketLines.BasketLineCreate;
public record BasketLineCreateCommand(
    Guid UserId,
    BasketLineDto? BasketLine) : IRequest<BasketLineCreateDto>;