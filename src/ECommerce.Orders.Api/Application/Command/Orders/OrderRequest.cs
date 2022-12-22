using MediatR;

namespace ECommerce.Orders.Api.Application.Command.Orders;

public class OrderRequest : IRequest<OrderResponse>
{
    public string? Name { get; set; }
}