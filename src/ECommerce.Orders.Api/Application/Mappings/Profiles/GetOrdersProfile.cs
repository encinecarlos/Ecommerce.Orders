using AutoMapper;
using ECommerce.Orders.Api.Domain.Entities;

namespace ECommerce.Orders.Api.Application.Mappings.Profiles;

public class GetOrdersProfile : Profile
{
    public GetOrdersProfile()
    {
        CreateMap<Order, GetOrdersMap>()
            .ForPath(source => source.ProductName, dest =>
                dest.MapFrom(d => d.Products.FirstOrDefault().ProductName))
            .ForPath(src => src.UnitPrice, dest =>
                dest.MapFrom(d => d.Products.FirstOrDefault().Price));
    }
}