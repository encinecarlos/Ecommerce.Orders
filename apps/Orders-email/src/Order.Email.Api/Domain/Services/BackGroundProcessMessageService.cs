using Order.Email.Api.Application.Notifications;
using Order.Email.Api.Application.Query;
using Order.Email.Api.Domain.Interfaces;
using Order.Email.Api.Domain.Services.Messagehandler;

namespace Order.Email.Api.Domain.Services;

public class BackGroundProcessMessageService : BackgroundService
{
    private ILogger<BackGroundProcessMessageService> Logger { get; }
    private IEventHandlerService EventConsumer { get; }
    private GetOrderById GetOrder { get; }
    private IMessageHandlerService MessageService { get; }
    public BackGroundProcessMessageService(
        ILogger<BackGroundProcessMessageService> logger, 
        IEventHandlerService eventConsumer, 
        GetOrderById getOrder, IMessageHandlerService messageService)
    {
        Logger = logger;
        EventConsumer = eventConsumer;
        GetOrder = getOrder;
        MessageService = messageService;
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
                    await MessageService.SendMessage(order);
                }

                Logger.LogInformation($"Message received: {result.OrderId}");
            }
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, "Unexpected error occurred!");
        }
    }
}