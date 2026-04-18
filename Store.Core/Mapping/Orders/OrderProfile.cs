using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.Core.Dtos.Auth;
using Store.Core.Dtos.Orders;
using Store.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile(IConfiguration configuration)
        {
            CreateMap<Order, OrderReturnDto>()
                .ForMember(d => d.DeliveryMethod, Options => Options.MapFrom(d => d.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, Options => Options.MapFrom(d => d.DeliveryMethod.Cost))
                ;

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, Options => Options.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, Options => Options.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, Options => Options.MapFrom(s => $"{configuration["BaseUrl"]}{s.Product.PictureUrl}"))
                ;

            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();


        }
    }
}
