using System.Text.Json;
using Confluent.Kafka;

namespace ECommerce.Orders.Api.Services.EventHandler;

public class SerializerService<T> : ISerializer<T>, IDeserializer<T>
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        using var ms = new MemoryStream();
        string jsonMessage = JsonSerializer.Serialize(data);
        var writer = new StreamWriter(ms);
        writer.Write(jsonMessage);
        writer.Flush();
        ms.Position = 0;

        return ms.ToArray();
    }

    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<T>(data.ToArray());
    }
}