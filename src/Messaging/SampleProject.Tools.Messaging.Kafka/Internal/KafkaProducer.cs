using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SampleProject.Tools.Messaging.Kafka.Abstractions;

namespace SampleProject.Tools.Messaging.Kafka.Internal;

internal class KafkaProducer<TValue> : KafkaProducer<string, TValue>, IKafkaProducer<TValue>
{
    internal KafkaProducer(
        KafkaProducerBuilderOptions<string, TValue> options,
        ILogger<KafkaProducer<string, TValue>> logger,
        IMessageFactory messageFactory) : base(options, logger, messageFactory)
    {
    }
}
