using System;

namespace SampleProject.Tools.Messaging.Kafka.Abstractions
{
    /// <summary>
    /// The Consumer Commit Strategy.
    /// </summary>
    /// <typeparam name="TKey">The Key of the Message.</typeparam>
    /// <typeparam name="TValue">The Value of the Message.</typeparam>
    public interface IKafkaCommitStrategy<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// Marks the number of Messages that Kafka should commit as processed and Commits.
        /// </summary>
        /// <param name="kafkaConsumer">The <see cref="IKafkaConsumer" /> the Commit Strategy should apply to.</param>
        /// <param name="lastProcessed">The Kafka Message that was processed last.</param>
        void MarkAsProcessed(IKafkaConsumer<TKey, TValue> kafkaConsumer, IKafkaMessage<TKey, TValue> lastProcessed);

        /// <summary>
        /// Immediately commits any messages held by the strategy.
        /// </summary>
        /// <param name="kafkaConsumer">The <see cref="IKafkaConsumer" /> the Commit Strategy should apply to.</param>
        void ForceCommit(IKafkaConsumer<TKey, TValue> kafkaConsumer);
    }
}
