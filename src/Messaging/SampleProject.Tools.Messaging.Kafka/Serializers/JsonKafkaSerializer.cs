using System.Text.Json;
using SampleProject.Tools.Messaging.Kafka.Abstractions;

namespace SampleProject.Tools.Messaging.Kafka.Serializers;

/// <summary>
/// The Json Kafka Serializer.
/// </summary>
public class JsonKafkaSerializer : IKafkaSerializer, IKafkaDeserializer
{
    /// <inheritdoc />
    public byte[] Serialize<T>(T data)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data);
    }

    /// <inheritdoc />
    public T Deserialize<T>(byte[] data)
    {
        return JsonSerializer.Deserialize<T>(data);
    }
}
