using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.OrderDtos;

namespace Warehouse.Application.Features.Commands.Order.OrderCreate;

public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, OrderCreateDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<OrderCreateDto> Handle(OrderCreateCommand command, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Domain.Entities.Orders.Order>(command);

        await UpdateProductSizesAsync(order);
        await _unitOfWork.Orders.Add(order);
        await _unitOfWork.SaveAsync();

        var checkedOrder = await _unitOfWork.Orders.GetById(order.Id);

        var orderDto = _mapper.Map<OrderCreateDto>(checkedOrder);

        return orderDto;
    }

    private async Task UpdateProductSizesAsync(Domain.Entities.Orders.Order order)
    {
        foreach (var orderLine in order.OrderLines)
        {
            var productSize = await _unitOfWork.ProductSizes
                .GetByCompositeKey(orderLine.ProductId, orderLine.SizeId);

            if (productSize != null)
            {
                productSize.QuantityInStock -= orderLine.Quantity;

                if (productSize.QuantityInStock <= 0)
                {
                    _unitOfWork.ProductSizes.Delete(productSize);
                }
                else
                {
                    _unitOfWork.ProductSizes.Update(productSize);
                }
            }
        }
    }
}