using AutoMapper;
using Warehouse.Application.Features.Commands.Product;
using Warehouse.Application.Features.Queries.Product;
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
        CreateMap<ProductQuery, ItemsDto>();
        CreateMap<CreateProductCommand, CreateProductDetailsDto>();
        CreateMap<Product, ProductDetailsDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.ProductGroups.Select(pg => pg.Group.Name).ToList()))
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(ps => new SizeDto { QuantityInStock = ps.QuantityInStock, Name = ps.Size.Name }).ToList()));

        CreateMap<Product, CreateProductDetailsDto>()
    .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
    .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.ProductGroups.Select(pg => pg.Group.Name).ToList()))
    .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(ps => new SizeDto { QuantityInStock = ps.QuantityInStock, Name = ps.Size.Name }).ToList()));
    }
}
