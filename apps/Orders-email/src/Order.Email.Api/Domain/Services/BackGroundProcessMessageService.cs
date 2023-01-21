using Order.Email.Api.Domain.Interfaces;

namespace Order.Email.Api.Domain.Services;

public class BackGroundProcessMessageService : BackgroundService
{
    private ILogger<BackGroundProcessMessageService> Logger { get; }
    private IEventHandlerService EventConsumer { get; }
    
    public BackGroundProcessMessageService(ILogger<BackGroundProcessMessageService> logger, IEventHandlerService eventConsumer)
    {
        Logger = logger;
        EventConsumer = eventConsumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("Process started...");
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Logger.LogInformation("Consume from topic...");
                await EventConsumer.ConsumeMessage(stoppingToken);
            }
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, "Unexpected error occurred!");
        }
    }
}