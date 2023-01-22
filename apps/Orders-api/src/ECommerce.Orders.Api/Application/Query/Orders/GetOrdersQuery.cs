using ECommerce.Orders.Api.Application.Dtos;
using MediatR;

namespace ECommerce.Orders.Api.Application.Query.Orders;

public class GetOrdersQuery : IRequest<IEnumerable<GetOrders>>
{
    
}