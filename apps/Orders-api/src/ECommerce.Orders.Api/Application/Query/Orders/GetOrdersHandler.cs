using AutoMapper;
using ECommerce.Orders.Api.Application.Dtos;
using ECommerce.Orders.Api.Domain.Entities;
using ECommerce.Orders.Api.Domain.Interfaces;
using MediatR;

namespace ECommerce.Orders.Api.Application.Query.Orders;

public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, IEnumerable<GetOrders>>
{
    public readonly ILogger<GetOrdersHandler> _logger;
    public readonly IOrdersRepository _ordersRepository;
    public readonly IMapper _mapper;

    public GetOrdersHandler(
        ILogger<GetOrdersHandler> logger,
        IOrdersRepository ordersRepository,
        IMapper mapper)
    {
        _logger = logger;
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<GetOrders>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _ordersRepository.GetAllOrdersAsync();
        var order = _mapper.Map<IEnumerable<Order>, IEnumerable<GetOrders>>(orders);

        return order;

        //return await Task.FromResult(new GetOrdersDto()
        //{
        //    Orders = order
        //});
    }
}