using ECommerce.Orders.Api.Application.Mappings;
using MediatR;

namespace ECommerce.Orders.Api.Application.Command.Orders;

public class AddOrderCommand : IRequest<OrderDto>
{
    public OrderRequestMap Order { get; set; }
}