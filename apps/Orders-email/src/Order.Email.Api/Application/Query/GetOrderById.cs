using Order.Email.Api.Domain.Interfaces;

namespace Order.Email.Api.Application.Query;

public class GetOrderById
{
    public IOrderRepository OrderRepository { get; set; }
    private ILogger<GetOrderById> Logger { get; }

    public GetOrderById(IOrderRepository orderRepository, ILogger<GetOrderById> logger)
    {
        OrderRepository = orderRepository;
        Logger = logger;
    }

    public async Task<Domain.Entities.Order> GetOrder(string orderId)
    {
        return await OrderRepository.GetOrderByIdAsync(orderId);
    }
}