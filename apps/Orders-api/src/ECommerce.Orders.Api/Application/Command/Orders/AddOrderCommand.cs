using ECommerce.Orders.Api.Application.Dtos;
using MediatR;

namespace ECommerce.Orders.Api.Application.Command.Orders;

public class AddOrderCommand : IRequest<OrderDto>
{
    public OrderRequest Order { get; set; }
}