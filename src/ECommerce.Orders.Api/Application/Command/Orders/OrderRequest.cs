using ECommerce.Orders.Api.Mappings;
using MediatR;

namespace ECommerce.Orders.Api.Application.Command.Orders;

public class OrderRequest : IRequest<OrderResponse>
{
    public OrderRequestMap? Order { get; set; }
}