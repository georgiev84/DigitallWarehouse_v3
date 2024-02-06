﻿using AutoMapper;
using Warehouse.Application.Features.Commands.Order.OrderCreate;
using Warehouse.Application.Features.Commands.Product.ProductCreate;
using Warehouse.Application.Features.Queries.Product.ProductList;
using Warehouse.Application.Models.Dto;
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
        CreateMap<ProductListQuery, ItemsDto>();
        CreateMap<ProductCreateCommand, OrderUpdateDto>();
        CreateMap<OrderCreateCommand, OrderCreateDto>();
        CreateMap<Product, ProductDetailsDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.ProductGroups.Select(pg => pg.Group.Name).ToList()))
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(ps => new SizeDto { QuantityInStock = ps.QuantityInStock, Name = ps.Size.Name }).ToList()));

        CreateMap<Product, CreateProductDetailsDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.ProductGroups.Select(pg => pg.Group.Name).ToList()))
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(ps => new SizeDto { QuantityInStock = ps.QuantityInStock, Name = ps.Size.Name }).ToList()));

        CreateMap<Product, UpdateProductDetailsDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.ProductGroups.Any() ? src.ProductGroups.Select(pg => pg.Group.Name).ToList() : null))
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Any() ? src.ProductSizes.Select(ps => new SizeDto { QuantityInStock = ps.QuantityInStock, Name = ps.Size.Name }).ToList() : null));

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));


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
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name ))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines.ToList()));

    }
}
