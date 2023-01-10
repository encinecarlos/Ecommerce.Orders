namespace ECommerce.Orders.Api.Domain.Interfaces;

public interface IEventHandlerService
{
    Task<bool> ProduceMessage<T>(string topic, T message);
}