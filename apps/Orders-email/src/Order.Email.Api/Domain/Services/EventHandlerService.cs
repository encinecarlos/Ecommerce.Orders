using Order.Email.Api.Domain.Interfaces;

namespace Order.Email.Api.Domain.Services;

public class EventHandlerService : IEventHandlerService
{
    public Task<bool> ConsumeMessage<T>(string topic, CancellationToken cancellationToken, Func<T> messageProcess)
    {
        throw new NotImplementedException();
    }
}