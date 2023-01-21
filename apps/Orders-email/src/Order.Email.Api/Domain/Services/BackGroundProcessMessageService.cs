using Order.Email.Api.Application.Notifications;
using Order.Email.Api.Application.Query;
using Order.Email.Api.Domain.Entities;
using Order.Email.Api.Domain.Interfaces;

namespace Order.Email.Api.Domain.Services;

public class BackGroundProcessMessageService : BackgroundService
{
    private ILogger<BackGroundProcessMessageService> Logger { get; }
    private IEventHandlerService EventConsumer { get; }
    private GetOrderById GetOrder { get; }
    public BackGroundProcessMessageService(
        ILogger<BackGroundProcessMessageService> logger, 
        IEventHandlerService eventConsumer, 
        GetOrderById getOrder)
    {
        Logger = logger;
        EventConsumer = eventConsumer;
        GetOrder = getOrder;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("Process started...");
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Logger.LogInformation("Consume from topic...");
                var result = await EventConsumer.ConsumeMessage<OrderMessage>(stoppingToken);

                var order = await GetOrder.GetOrder(result.OrderId);
                {
                    var orderFound = new
                    {
                        OrderId = order.OrderId,
                        customername = order.Customer.Name,
                        email = order.Customer.Email
                    };
                }

                Logger.LogInformation($"Message received: {result}");
            }
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, "Unexpected error occurred!");
        }
    }
}