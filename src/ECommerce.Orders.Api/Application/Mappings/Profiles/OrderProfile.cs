using AutoMapper;
using ECommerce.Orders.Api.Domain.Entities;

namespace ECommerce.Orders.Api.Application.Mappings.Profiles;

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