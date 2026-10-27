
using AutoMapper;
using DomainLayer.Entities;
using Shared.DataTransferObjects;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(
                    dest => dest.BrandName,
                    opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(
                    dest => dest.TypeName,
                    opt => opt.MapFrom(src => src.ProductType.Name));

            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();
        }
    }
}
