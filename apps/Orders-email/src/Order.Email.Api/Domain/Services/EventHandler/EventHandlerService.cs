using System.Text.Json.Serialization;
using Confluent.Kafka;
using Newtonsoft.Json;
using Order.Email.Api.Domain.Entities;
using Order.Email.Api.Domain.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Order.Email.Api.Domain.Services.EventHandler;

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
            BootstrapServers = Configuration["Events:BootstrapServers"],
            GroupId = Configuration["Events:GroupId"],
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        
        BuilderConsumer = new ConsumerBuilder<Ignore, string>(ConsumerConfig)
            .Build();
        
        BuilderConsumer.Subscribe(Configuration["Events:Topic"]);
    }

    public Task<T?> ConsumeMessage<T>(CancellationToken cancellationToken)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var consume = BuilderConsumer.Consume(cancellationTokenSource.Token);
        var result = JsonSerializer.Deserialize<T>(consume.Message.Value);

        Logger.LogInformation("Return message to the caller");
        return Task.FromResult(result);
    }
}