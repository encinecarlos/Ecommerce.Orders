using AutoMapper;
using ECommerce.Orders.Api.Entities;
using ECommerce.Orders.Api.Mappings;
using ECommerce.Orders.Api.Repositories;
using MediatR;

namespace ECommerce.Orders.Api.Application.Query.Orders;

public class GetOrdersQuery : IRequestHandler<GetOrdersRequest, GetOrdersResponse>
{
    public ILogger<GetOrdersQuery> Logger { get; set; }
    public IOrdersRepository OrdersRepository { get; set; }
    public IMapper Mapper { get; set; }

    public GetOrdersQuery(ILogger<GetOrdersQuery> logger, IOrdersRepository ordersRepository, IMapper mapper)
    {
        Logger = logger;
        OrdersRepository = ordersRepository;
        Mapper = mapper;
    }


    public async Task<GetOrdersResponse> Handle(GetOrdersRequest request, CancellationToken cancellationToken)
    {
        var orders = await OrdersRepository.GetAllOrdersAsync();
        var order = Mapper.Map<IEnumerable<Order>, IEnumerable<GetOrdersMap>>(orders);

        return await Task.FromResult(new GetOrdersResponse()
        {
            Orders = order
        });
    }
}