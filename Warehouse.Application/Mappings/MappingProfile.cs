using AutoMapper;
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
        CreateMap<ProductCreateCommand, CreateProductDetailsDto>();
        CreateMap<CreateOrderCommand, CreateOrderDto>();
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
    }
}
