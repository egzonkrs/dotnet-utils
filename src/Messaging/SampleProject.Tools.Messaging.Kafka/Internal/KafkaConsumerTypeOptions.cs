using System;
using Polly;

namespace SampleProject.Tools.Messaging.Kafka.Internal;

internal class KafkaConsumerTypeOptions
{
    internal Type MessageType { get; set; }

    internal IAsyncPolicy MessageHandlerPolicy { get; set; } = Policy.NoOpAsync();

    // internal IAsyncPolicy ErrorHandlerPolicy { get; set; } =
    //     RetryPolicies.ExponentialBackOffRetryForeverAsync<Exception>(maxBackOffAmount: 6);
}
