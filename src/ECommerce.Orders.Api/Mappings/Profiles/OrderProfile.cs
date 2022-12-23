using AutoMapper;
using ECommerce.Orders.Api.Entities;

namespace ECommerce.Orders.Api.Mappings.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderRequestMap, Order>()
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingDate, opt => opt.Ignore())
            .ForMember(dest => dest.OrderStatus, opt => opt.Ignore());
    }
}