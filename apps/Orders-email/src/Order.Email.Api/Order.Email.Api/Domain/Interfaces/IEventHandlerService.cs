namespace Order.Email.Api.Domain.Interfaces;

public interface IEventHandlerService
{
    Task<bool> ConsumeMessage<T>(string topic, CancellationToken cancellationToken, Func<T> messageProcess);
}