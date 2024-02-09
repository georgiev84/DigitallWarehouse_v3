using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Factories;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.BasketDtos;
using Warehouse.Domain.Exceptions;

namespace Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;
public class BasketLineCreateCommandHandler : IRequestHandler<BasketLineCreateCommand, BasketLineCreateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IBasketLineFactory _basketLineFactory;
    public BasketLineCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IBasketLineFactory basketLineFactory)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _basketLineFactory = basketLineFactory ?? throw new ArgumentNullException(nameof(basketLineFactory));
    }

    public async Task<BasketLineCreateDto> Handle(BasketLineCreateCommand command, CancellationToken cancellationToken)
    {
        var existingBasket = await _unitOfWork.Baskets.GetSingleBasketByUserIdAsync(command.UserId);
        if (existingBasket == null)
        {
            throw new BasketNotFoundException($"Basket for User with Id {command.UserId} not found.");
        }

        var existingBasketLine = await _unitOfWork.BasketLines.GetById(existingBasket.Id);

        if (existingBasketLine != null)
        {
            // Already exist - Do something
        }

        var basketLine = _basketLineFactory.CreateBasketLine(command);
        basketLine.BasketId = existingBasket.Id;

        await _unitOfWork.BasketLines.Add(basketLine);
        _unitOfWork.SaveAsync();

        var baskeLineDto = _mapper.Map<BasketLineCreateDto>(basketLine);
        return baskeLineDto;
    }
}
