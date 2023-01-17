namespace Order.Email.Api.Domain.Services;

public class BackGroundProcessMessage : BackgroundService
{
    private ILogger<BackGroundProcessMessage> _logger;


    public BackGroundProcessMessage(ILogger<BackGroundProcessMessage> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Process started...");
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Consume from topic...");
            await Task.Delay(1000, stoppingToken);
        }
    }
}