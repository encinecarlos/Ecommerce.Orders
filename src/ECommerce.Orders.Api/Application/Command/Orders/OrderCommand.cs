using AutoMapper;
using ECommerce.Orders.Api.Entities;
using ECommerce.Orders.Api.Mappings;
using MediatR;

namespace ECommerce.Orders.Api.Application.Command.Orders;

public class OrderCommand : IRequestHandler<OrderRequest, OrderResponse>
{
    private ILogger<OrderCommand> Logger { get; }
    private IMapper Mapper;

    public OrderCommand(ILogger<OrderCommand> logger, IMapper mapper)
    {
        Logger = logger;
        Mapper = mapper;
    }

    public async Task<OrderResponse> Handle(OrderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = Mapper.Map<Order>(request.Order);
            
            Logger.LogInformation("Start process of order");
            return await Task.FromResult(new OrderResponse()
            {
                ResponseMessage = $"Order generated at {result.OrderDate:dd/MM/yyyy hh:mm:ss}",
                orderId = result.OrderId
            });
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            throw;
        }
    }
}