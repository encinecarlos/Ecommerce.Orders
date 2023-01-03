using ECommerce.Orders.Api.Application.Mappings;

namespace ECommerce.Orders.Api.Application.Query.Orders;

public class GetOrdersDto
{
    public IEnumerable<GetOrdersMap> Orders { get; set; }
}