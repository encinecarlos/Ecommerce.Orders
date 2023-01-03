using AutoMapper;
using ECommerce.Orders.Api.Application.Notifications;
using ECommerce.Orders.Api.Entities;
using ECommerce.Orders.Api.Repositories;
using ECommerce.Orders.Api.Services.EventHandler;
using MediatR;

namespace ECommerce.Orders.Api.Application.Command.Orders;

public class OrderCommand : IRequestHandler<OrderRequest, OrderResponse>
{
    private ILogger<OrderCommand> Logger { get; }
    private IMapper Mapper;
    private IOrdersRepository OrdersRepository { get; }
    private IEventHandlerService EventHandlerService { get; }

    public OrderCommand(ILogger<OrderCommand> logger, IMapper mapper, IOrdersRepository ordersRepository, IEventHandlerService eventHandlerService)
    {
        Logger = logger;
        Mapper = mapper;
        OrdersRepository = ordersRepository;
        EventHandlerService = eventHandlerService;
    }

    public async Task<OrderResponse> Handle(OrderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = Mapper.Map<Order>(request.Order);
            
            Logger.LogInformation("Save the order");

            await OrdersRepository.AddOrderAsync(result);
            
            Logger.LogInformation("Produce event to process the order");
            var orderNotification = new OrderNotification()
            {
                OrderId = result.OrderId,
                OrderDate = result.OrderDate
            };

            await EventHandlerService.ProduceMessage("send-email", orderNotification);
            
            Logger.LogInformation("Send response to client");
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