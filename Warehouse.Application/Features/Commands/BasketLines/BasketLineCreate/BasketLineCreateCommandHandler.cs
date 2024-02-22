using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.BasketDtos;
using Warehouse.Domain.Exceptions.BasketExceptions;
using Warehouse.Domain.Entities.Baskets;

namespace Warehouse.Application.Features.Commands.BasketLines.BasketLineCreate;

public class BasketLineCreateCommandHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<BasketLineCreateCommand, BasketLineCreateDto>
{
    public async Task<BasketLineCreateDto> Handle(BasketLineCreateCommand command, CancellationToken cancellationToken)
    {
        var existingBasket = await _unitOfWork.Baskets.GetSingleBasketByUserIdAsync(command.UserId);

        var basketLine = _mapper.Map<BasketLine>(command);
        basketLine.BasketId = existingBasket.Id;

        await _unitOfWork.BasketLines.Add(basketLine);
        await _unitOfWork.SaveAsync();

        var baskeLineDto = _mapper.Map<BasketLineCreateDto>(basketLine);
        return baskeLineDto;
    }
}