using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.BasketDtos;
using Warehouse.Domain.Exceptions;
using Warehouse.Persistence.EF.Factories;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;

public class BasketLineCreateCommandHandler : IRequestHandler<BasketLineCreateCommand, BasketLineCreateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BasketLineCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BasketLineCreateDto> Handle(BasketLineCreateCommand command, CancellationToken cancellationToken)
    {
        var existingBasket = await _unitOfWork.Baskets.GetSingleBasketByUserIdAsync(command.UserId);

        var existingBasketLine = await _unitOfWork.BasketLines.GetById(existingBasket.Id);

        if (existingBasketLine is not null)
        {
            throw new BasketLineExistException("BasketLine already exists.");
        }

        var basketLine = BasketLineHelper.CreateBasketLine(command);
        basketLine.BasketId = existingBasket.Id;

        await _unitOfWork.BasketLines.Add(basketLine);
        await _unitOfWork.SaveAsync();

        var baskeLineDto = _mapper.Map<BasketLineCreateDto>(basketLine);
        return baskeLineDto;
    }
}