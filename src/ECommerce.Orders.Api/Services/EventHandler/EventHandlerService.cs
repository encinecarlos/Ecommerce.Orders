using Confluent.Kafka;

namespace ECommerce.Orders.Api.Services.EventHandler;

public class EventHandlerService : IEventHandlerService
{
    private IConfiguration Configuration { get; }
    private ProducerConfig ProducerConfig { get; }
    private ILogger<EventHandlerService> Logger { get; }

    public EventHandlerService(IConfiguration configuration, ILogger<EventHandlerService> logger)
    {
        Configuration = configuration;
        Logger = logger;
        ProducerConfig = new ProducerConfig()
        {
            BootstrapServers = Configuration["Events:BootstrapServers"]
        };
    }

    public async Task<bool> ProduceMessage<T>(string topic, T message)
    {
        var messageContent = new Message<Null, T>()
        {
            Value = message
        };

        using var producer = new ProducerBuilder<Null, T>(ProducerConfig)
            .SetValueSerializer(new SerializerService<T>())
            .Build();
        
        var sendMessage = await producer.ProduceAsync(topic, messageContent);
        producer.Flush(TimeSpan.FromSeconds(5));
        return sendMessage.Status == PersistenceStatus.Persisted;
    }
}