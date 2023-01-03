namespace ECommerce.Orders.Api.Services.EventHandler;

public interface IEventHandlerService
{
    Task<bool> ProduceMessage<T>(string topic, T message);
}