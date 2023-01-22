namespace Order.Email.Api.Domain.Interfaces;

public interface IEventHandlerService
{
    Task<T?> ConsumeMessage<T>(CancellationToken cancellationToken);
}