using Confluent.Kafka;
using Order.Email.Api.Domain.Interfaces;

namespace Order.Email.Api.Domain.Services;

public class EventHandlerService : IEventHandlerService
{
    private IConfiguration Configuration { get; }
    private ILogger<EventHandlerService> Logger { get; }
    private ConsumerConfig ConsumerConfig { get; }
    private IConsumer<Ignore, string> BuilderConsumer { get; }

    public EventHandlerService(
        IConfiguration configuration, 
        ILogger<EventHandlerService> logger)
    {
        Configuration = configuration;
        Logger = logger;
        ConsumerConfig = new()
        {
            BootstrapServers = Configuration["Events:BootrapServer"],
            GroupId = Configuration["Events:GropuId"],
        };
        
        BuilderConsumer = new ConsumerBuilder<Ignore, string>(ConsumerConfig).Build();
        
        BuilderConsumer.Subscribe(configuration["Events:Topic"]);
    }

    public Task<string> ConsumeMessage(CancellationToken cancellationToken)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var consume = BuilderConsumer.Consume(cancellationTokenSource.Token);

        return Task.FromResult(consume.Message.Value);
    }
}