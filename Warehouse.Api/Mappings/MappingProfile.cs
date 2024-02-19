using AutoMapper;
using Warehouse.Api.Models.OrderResponses.Orders;
using Warehouse.Api.Models.Requests;
using Warehouse.Api.Models.Requests.Basket;
using Warehouse.Api.Models.Requests.BasketLine;
using Warehouse.Api.Models.Requests.Orders;
using Warehouse.Api.Models.Requests.Product;
using Warehouse.Api.Models.Responses.BasketResponses;
using Warehouse.Api.Models.Responses.OrderResponses;
using Warehouse.Api.Models.Responses.ProductResponses;
using Warehouse.Application.Features.Commands.BasketLine.BasketLineBulkDelete;
using Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;
using Warehouse.Application.Features.Commands.BasketLine.BasketLineDelete;
using Warehouse.Application.Features.Commands.BasketLine.BasketLineUpdate;
using Warehouse.Application.Features.Commands.Order.OrderCreate;
using Warehouse.Application.Features.Commands.Order.OrderDelete;
using Warehouse.Application.Features.Commands.Order.OrderUpdate;
using Warehouse.Application.Features.Commands.Product.ProductCreate;
using Warehouse.Application.Features.Commands.Product.Update;
using Warehouse.Application.Features.Queries.Basket.BasketSingleQuery;
using Warehouse.Application.Features.Queries.Order.OrderGetAll;
using Warehouse.Application.Features.Queries.Order.OrderGetSingle;
using Warehouse.Application.Features.Queries.Product.ProductList;
using Warehouse.Application.Models.Dto;
using Warehouse.Application.Models.Dto.BasketDtos;
using Warehouse.Application.Models.Dto.OrderDtos;
using Warehouse.Application.Models.Dto.ProductDtos;
using Warehouse.Domain.Entities.Orders;

namespace Warehouse.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapFromRequestToCommands();
        MapFromDtoToResponse();
        MapFromRequestToQueries();
    }

    private void MapFromRequestToQueries()
    {
        CreateMap<ProductFilterRequest, ProductListGetQuery>();
        CreateMap<OrderRequest, OrderGetAllQuery>();
        CreateMap<OrderSingleRequest, OrderGetSingleQuery>();
        CreateMap<BasketSingleRequest, BasketSingleQuery>();
    }

    private void MapFromRequestToCommands()
    {
        CreateMap<ProductCreateRequest, ProductCreateCommand>();
        CreateMap<ProductUpdateRequest, ProductUpdateCommand>();
        CreateMap<OrderCreateRequest, OrderCreateCommand>();
        CreateMap<OrderLineRequest, OrderLine>();
        CreateMap<OrderUpdateRequest, OrderUpdateCommand>();
        CreateMap<OrderLineUpdateRequest, OrderLine>();
        CreateMap<OrderSingleRequest, OrderGetSingleQuery>();
        CreateMap<OrderDeleteRequest, OrderDeleteCommand>();
        CreateMap<BasketLineCreateRequest, BasketLineCreateCommand>();
        CreateMap<BasketLineUpdateRequest, BasketLineUpdateCommand>();
        CreateMap<BasketLineDeleteRequest, BasketLineDeleteCommand>();
        CreateMap<BasketLineBulkDeleteRequest, BasketLineBulkDeleteCommand>();
        CreateMap<BasketLineCreateRequest, BasketLineCreateCommand>();
    }

    private void MapFromDtoToResponse()
    {
        CreateMap<ProductDto, ProductDetailedResponse>();
        CreateMap<SizeInformationRequest, SizeInformationDto>();
        CreateMap<SizeDto, SizeResponse>();
        CreateMap<OrderUpdateDto, ProductCreateResponse>();
        CreateMap<ProductUpdateDetailsDto, ProductUpdateResponse>();
        CreateMap<OrderDto, OrderResponse>();
        CreateMap<OrderCreateDto, OrderCreateResponse>();
        CreateMap<OrderUpdateDto, OrderUpdateResponse>();
        CreateMap<OrderLineDto, OrderLineResponse>();
        CreateMap<OrderWithDetailsDto, OrderDetailedResponse>();
        CreateMap<OrderUpdateDto, OrderUpdateResponse>();
        CreateMap<OrderUpdateDto, OrderDetailedResponse>();
        CreateMap<BasketCreateDto, BasketCreateResponse>();
        CreateMap<BasketLineCreateDto, BasketLineResponse>();
        CreateMap<BasketLineCreateDto, BasketLineDto>();
        CreateMap<BasketDetailDto, BasketResponse>();
        CreateMap<BasketLineUpdateDto, BasketLineUpdateResponse>();
        CreateMap<OrderUpdateDto, OrderResponse>();
        CreateMap<ProductCreateDetailsDto, ProductCreateResponse>();
    }
}