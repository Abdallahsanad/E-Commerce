using AutoMapper;
using Store.Core.Dtos.Products;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Store.Core.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration ) 
        {
            CreateMap<Product, ProductsDto>()
                .ForMember(d => d.BrandName, options => options.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.TypeName, options => options.MapFrom(s => s.Type.Name))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(s => $"{configuration["BaseUrl"]}{s.PictureUrl}"));

            CreateMap<ProductBrand, TypeBrandDto>();
            CreateMap<ProductType, TypeBrandDto>();
            CreateMap<TypeBrandDto, ProductType>();
            CreateMap<TypeBrandDto, ProductBrand>();

            CreateMap<ProductsDto, Product>();

            CreateMap<Product, CreateProductsDto>()
                .ForMember(d => d.BrandName, options => options.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.TypeName, options => options.MapFrom(s => s.Type.Name));

            CreateMap<CreateProductsDto, Product>();



        }
    }
}
