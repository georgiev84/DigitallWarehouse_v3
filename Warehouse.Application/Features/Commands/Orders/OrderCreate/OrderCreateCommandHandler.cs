using AutoMapper;
using MediatR;
using Warehouse.Application.Common.Interfaces.Persistence;
using Warehouse.Application.Models.Dto.OrderDtos;
using Warehouse.Domain.Entities.Orders;

namespace Warehouse.Application.Features.Commands.Orders.OrderCreate;

public class OrderCreateCommandHandler(IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<OrderCreateCommand, OrderCreateDto>
{
    public async Task<OrderCreateDto> Handle(OrderCreateCommand command, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(command);

        await UpdateProductSizesAsync(order);
        await _unitOfWork.Orders.Add(order);
        await _unitOfWork.SaveAsync();

        var checkedOrder = await _unitOfWork.Orders.GetById(order.Id);

        var orderDto = _mapper.Map<OrderCreateDto>(checkedOrder);

        return orderDto;
    }

    private async Task UpdateProductSizesAsync(Order order)
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
                    await _unitOfWork.ProductSizes.Update(productSize);
                }
            }
        }
    }
}