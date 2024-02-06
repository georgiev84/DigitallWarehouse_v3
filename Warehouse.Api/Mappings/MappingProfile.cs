using AutoMapper;
using Warehouse.Application.Models.Dto;
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
using Warehouse.Api.Models.OrderResponses.Orders;
using Warehouse.Api.Models.Responses.ProductResponses;
using Warehouse.Api.Models.Responses.OrderResponses;


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
        CreateMap<OrderLineUpdateRequest, OrderLine>();
    }

    private void MapFromDtoToResponse()
    {
        CreateMap<ProductDto, ProductDetailedResponse>();
        CreateMap<SizeInformationRequest, SizeInformationDto>();
        CreateMap<SizeDto, SizeResponse>();
        CreateMap<OrderUpdateDto, ProductCreateResponse>();
        CreateMap<UpdateProductDetailsDto, ProductUpdateResponse>();
        CreateMap<OrderDto, OrderResponse>();
        CreateMap<OrderCreateDto, OrderCreateResponse>();
        CreateMap<OrderUpdateDto, OrderUpdateResponse>();
        CreateMap<OrderLineDto, OrderLineResponse>();
        CreateMap<OrderWithDetailsDto, OrderDetailedResponse>();
        CreateMap<OrderUpdateDto, OrderUpdateResponse>();
        CreateMap<OrderUpdateDto, OrderDetailedResponse>()
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));
        
    }
}
