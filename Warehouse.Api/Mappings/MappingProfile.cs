using AutoMapper;
using Warehouse.Application.Models.Dto;
using Warehouse.Api.Models.Responses;
using Warehouse.Api.Models.Requests;
using Warehouse.Api.Models.Requests.Product;
using Warehouse.Api.Models.Requests.Orders;
using Warehouse.Application.Features.Commands.Product.ProductCreate;
using Warehouse.Application.Features.Commands.Product.Update;
using Warehouse.Application.Features.Commands.Order.OrderCreate;
using Warehouse.Application.Features.Queries.Order.OrderGetAll;
using Warehouse.Application.Features.Queries.Order.OrderGetSingle;
using Warehouse.Application.Features.Queries.Product.ProductList;
using Warehouse.Application.Features.Commands.Order.OrderUpdate;
using Warehouse.Domain.Entities;

namespace Warehouse.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapFromRequestToQueriesOrCommands();
        MapFromDtoToResponse();
    }

    private void MapFromRequestToQueriesOrCommands()
    {
        CreateMap<ProductFilterRequest, ProductListQuery>();
        CreateMap<ProductCreateRequest, ProductCreateCommand>();
        CreateMap<ProductUpdateRequest, ProductUpdateCommand>();
        CreateMap<OrderRequest, OrderGetAllQuery>();
        CreateMap<OrderSingleRequest, OrderGetSingleQuery>();
        CreateMap<OrderCreateRequest, OrderCreateCommand>();
        CreateMap<OrderUpdateRequest, OrderUpdateCommand>();
        CreateMap<OrderLinesUpdateRequest, OrderDetails>();
    }

    private void MapFromDtoToResponse()
    {
        CreateMap<ProductDto, ProductResponse>();
        CreateMap<SizeInformationRequest, SizeInformationDto>();
        CreateMap<SizeDto, SizeResponse>();
        CreateMap<OrderUpdateDto, CreateProductResponse>();
        CreateMap<UpdateProductDetailsDto, UpdateProductResponse>();
        CreateMap<OrderDto, OrderResponse>();
        CreateMap<OrderCreateDto, CreateOrderResponse>();
        CreateMap<OrderUpdateDto, OrderUpdateResponse>();
        CreateMap<OrderLinesDto, OrderLineResponse>();
        CreateMap<OrderWithDetailsDto, OrderWithDetailsResponse>();
        CreateMap<OrderUpdateDto, OrderUpdateResponse>();
        CreateMap<OrderUpdateDto, OrderWithDetailsResponse>()
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderDetails));
        
    }
}
