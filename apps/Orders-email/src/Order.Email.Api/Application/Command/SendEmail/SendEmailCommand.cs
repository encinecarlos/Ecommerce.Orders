using Order.Email.Api.Application.Notifications;
using Order.Email.Api.Application.Query;
using Order.Email.Api.Domain.Interfaces;

namespace Order.Email.Api.Application.Command.SendEmail;

public class SendEmailCommand
{
    private ILogger<SendEmailCommand> Logger { get; }
    private IEventHandlerService EventConsumer { get; }
    private GetOrderById GetOrder { get; }
    private IMessageHandlerService MessageService { get; }

    public SendEmailCommand(
        ILogger<SendEmailCommand> logger, 
        IEventHandlerService eventConsumer, 
        GetOrderById getOrder, 
        IMessageHandlerService messageService)
    {
        Logger = logger;
        EventConsumer = eventConsumer;
        GetOrder = getOrder;
        MessageService = messageService;
    }

    public async Task SendCustomerMessage()
    {
        Logger.LogInformation("Get orderid from topic");
        var orderMessage = await EventConsumer.ConsumeMessage<OrderMessage>(new CancellationToken());
        
        Logger.LogInformation($"Get order from database");
        var orderData = await GetOrder.GetOrder(orderMessage.OrderId);

        var emailAdapted = EmailAdapter.Adapt(orderData);

        Logger.LogInformation("Send message to customer");
        await MessageService.SendMessage(emailAdapted);
    }
}