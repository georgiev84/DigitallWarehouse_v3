using AutoMapper;
using Warehouse.Application.Features.Commands.BasketLine.BasketLineCreate;
using Warehouse.Application.Features.Commands.BasketLine.BasketLineUpdate;
using Warehouse.Application.Features.Commands.Order.OrderCreate;
using Warehouse.Application.Features.Commands.Order.OrderUpdate;
using Warehouse.Application.Features.Commands.Product.ProductCreate;
using Warehouse.Application.Features.Queries.Product.ProductList;
using Warehouse.Application.Models.Dto;
using Warehouse.Application.Models.Dto.BasketDtos;
using Warehouse.Application.Models.Dto.OrderDtos;
using Warehouse.Application.Models.Dto.ProductDtos;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        MapFromQueryCommandEntityToDto();
    }

    private void MapFromQueryCommandEntityToDto()
    {
        CreateMap<ProductListGetQuery, ItemsDto>();
        CreateMap<ProductCreateCommand, OrderUpdateDto>();
        CreateMap<SizeInformationDto, ProductSize>()
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<ProductCreateCommand, Product>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.ProductSizes, opt => opt.MapFrom(src => src.SizeInformation))
            .ForMember(dest => dest.ProductGroups, opt => opt.MapFrom(src => src.GroupIds));

        CreateMap<OrderCreateCommand, OrderCreateDto>();
        CreateMap<Product, ProductDetailsDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.ProductGroups.Select(pg => pg.Group.Name).ToList()))
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(ps => new SizeDto { QuantityInStock = ps.QuantityInStock, Name = ps.Size.Name }).ToList()));

        CreateMap<Product, ProductCreateDetailsDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.ProductGroups.Select(pg => pg.Group.Name).ToList()))
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(ps => new SizeDto { QuantityInStock = ps.QuantityInStock, Name = ps.Size.Name }).ToList()));

        CreateMap<Product, ProductUpdateDetailsDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.ProductGroups.Any() ? src.ProductGroups.Select(pg => pg.Group.Name).ToList() : null))
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Any() ? src.ProductSizes.Select(ps => new SizeDto { QuantityInStock = ps.QuantityInStock, Name = ps.Size.Name }).ToList() : null));

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));

        CreateMap<OrderUpdateCommand, Order>();

        CreateMap<Order, OrderWithDetailsDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));

        CreateMap<OrderLine, OrderLineDto>()
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.Name));

        CreateMap<Order, OrderCreateDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

        CreateMap<Order, OrderUpdateDto>()
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentId ?? Guid.Empty))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId ?? Guid.Empty))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));

        CreateMap<OrderCreateCommand, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));

        CreateMap<BasketLineCreateCommand, BasketLine>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.BasketId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.BasketLine.ProductId))
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.BasketLine.SizeId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.BasketLine.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.BasketLine.Price))
            .ForMember(dest => dest.Basket, opt => opt.Ignore()) 
            .ForMember(dest => dest.Product, opt => opt.Ignore()) 
            .ForMember(dest => dest.Size, opt => opt.Ignore());

        CreateMap<BasketLine, BasketLineDto>()
            .ForMember(
                 dest => dest.ProductName,
                 opt => opt.MapFrom(src => src.Product.Title))
             .ForMember(
                 dest => dest.SizeName,
                 opt => opt.MapFrom(src => src.Size.Name));

        CreateMap<BasketLine, BasketLineCreateDto>();

        CreateMap<Basket, BasketDetailDto>()
             .ForMember(
                 dest => dest.Id,
                 opt => opt.MapFrom(src => src.Id))
             .ForMember(
                 dest => dest.UserId,
                 opt => opt.MapFrom(src => src.UserId))
             .ForMember(
                 dest => dest.FullName,
                 opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
             .ForMember(
                 dest => dest.TotalAmount,
                 opt => opt.MapFrom(src => src.BasketLines.Sum(bl => bl.Quantity * bl.Price)));

        CreateMap<BasketLineUpdateCommand, BasketLine>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BasketLineId));

        CreateMap<BasketLine, BasketLineUpdateDto>()
            .ForMember(dest => dest.BasketLineId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
    }
}