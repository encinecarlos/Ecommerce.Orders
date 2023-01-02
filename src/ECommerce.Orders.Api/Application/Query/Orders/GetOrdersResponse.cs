using ECommerce.Orders.Api.Mappings;

namespace ECommerce.Orders.Api.Application.Query.Orders;

public class GetOrdersResponse
{
    public IEnumerable<GetOrdersMap> Orders { get; set; }
}