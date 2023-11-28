using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SampleProject.Utils.Messaging.Kafka.Abstractions;

namespace SampleProject.Utils.Messaging.Kafka.Internal;

internal class KafkaProducer<TValue> : KafkaProducer<string, TValue>, IKafkaProducer<TValue>
{
    internal KafkaProducer(
        KafkaProducerBuilderOptions<string, TValue> options,
        ILogger<KafkaProducer<string, TValue>> logger,
        IMessageFactory messageFactory) : base(options, logger, messageFactory)
    {
    }
}
