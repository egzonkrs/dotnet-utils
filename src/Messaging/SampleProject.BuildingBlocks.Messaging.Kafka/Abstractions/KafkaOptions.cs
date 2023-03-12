using Confluent.Kafka;
using System.Collections.Generic;

namespace SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

/// <summary>
/// The Kafka Options.
/// </summary>
public class KafkaOptions
{
    /// <summary>
    /// The configuration default section name.
    /// </summary>
    public const string DefaultSectionName = "Messaging:Kafka";

    /// <summary>
    /// List of the Bootstrap servers.
    /// </summary>
    public string Brokers { get; set; } = "localhost:9093";

    /// <summary>
    /// The group id.
    /// </summary>
    public string ConsumerGroupId { get; set; } = "default-group";

    /// <summary>
    /// Message timeout in milliseconds.
    /// </summary>
    public int? MessageTimeoutMs { get; set; }

    /// <summary>
    /// AutoOffsetRest enum value.
    /// </summary>
    public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Earliest;

    /// <summary>
    /// Enable auto-commit.
    /// </summary>
    public bool EnableAutoCommit { get; set; } = false;

    /// <summary>
    /// Security options for Kafka Provider.
    /// </summary>
    public KafkaSecurityOptions Security { get; set; } = new KafkaSecurityOptions();

    /// <summary>
    /// Additional configuration.
    /// </summary>
    public IDictionary<string, string> AdditionalConfiguration { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// Delay in milliseconds to wait for messages in the producer queue.
    /// </summary>
    public double? LingerMs { get; set; } = 0;

    /// <summary>
    /// Max number of messages batched in one message set.
    /// </summary>
    public int? BatchNumMessages { get; set; } = null;

    /// <summary>
    /// Max number of messages allowed in the producer queue.
    /// </summary>
    public int? QueueBufferingMaxMessages { get; set; } = null;

    /// <summary>
    /// Min time the broker can wait to fill response.
    /// </summary>
    public int? FetchWaitMaxMax { get; set; } = null;

    /// <summary>
    /// Maximum allowed time between calls to consume messages for high-level consumers.
    /// </summary>
    public int? MaxPollIntervalMs { get; set; } = null;

    /// <summary>
    /// Group session keep alive heartbeat interval.
    /// </summary>
    public int? HeartbeatIntervalMs { get; set; } = null;

    /// <summary>
    /// Default timeout for network requests.
    /// </summary>
    public int? SocketTimeoutMs { get; set; } = null;

    /// <summary>
    /// Log broker disconnects. It might be useful to turn this off when interacting
    /// with 0.9 brokers with an aggressive `connection.max.idle.ms` value.
    /// </summary>
    public bool? LogConnectionClose { get; set; } = true;

    /// <summary>
    /// Disable the Nagle algorithm on broker sockets.
    /// </summary>
    public bool? SocketNagleDisable { get; set; } = true;

    /// <summary>
    /// Enable TCP keep alive on broker sockets.
    /// </summary>
    public bool? SocketKeepaliveEnable { get; set; } = true;

    /// <summary>
    /// Debug contexts to enable. Comma separated.
    /// </summary>
    public string Debug { get; set; } = null;

    internal void ApplyProperties(ClientConfig config)
    {
        config.BootstrapServers = Brokers.Replace(";", ",");
        config.SocketNagleDisable = SocketNagleDisable;
        config.SocketKeepaliveEnable = SocketKeepaliveEnable;
        config.SocketTimeoutMs = SocketTimeoutMs;
        config.LogConnectionClose = LogConnectionClose;
        config.Debug = Debug;

        if (Security?.IsEnabled is true)
        {
            config.SecurityProtocol = Security.SecurityProtocol;

            config.SaslUsername = Security.SaslUsername;
            config.SaslPassword = Security.SaslPassword;
            config.SaslMechanism = Security.SaslMechanism;

            config.SslCaLocation = Security.SslCaLocation;
            config.SslCertificateLocation = Security.SslCertificateLocation;
            config.EnableSslCertificateVerification = Security.EnableSslCertificateVerification;
            config.SslKeystoreLocation = Security.SslKeystoreLocation;
            config.SslKeystorePassword = Security.SslKeystorePassword;
            config.SslKeyLocation = Security.SslKeyLocation;
            config.SslKeyPassword = Security.SslKeyPassword;
        }
    }

    /// <summary>
    /// Maps configuration to Confluent <see cref="ConsumerConfig"/>.
    /// </summary>
    public ConsumerConfig ToConsumerProperties()
    {
        var config = new ConsumerConfig(new Dictionary<string, string>(AdditionalConfiguration))
        {
            GroupId = ConsumerGroupId,
            AutoOffsetReset = AutoOffsetReset,
            EnableAutoCommit = EnableAutoCommit,
            FetchWaitMaxMs = FetchWaitMaxMax,
            MaxPollIntervalMs = MaxPollIntervalMs,
            HeartbeatIntervalMs = HeartbeatIntervalMs
        };

        ApplyProperties(config);

        return config;
    }

    /// <summary>
    /// Maps configuration to Confluent <see cref="ProducerConfig"/>.
    /// </summary>
    public ProducerConfig ToProducerProperties()
    {
        var config = new ProducerConfig(new Dictionary<string, string>(AdditionalConfiguration))
        {
            LingerMs = LingerMs,
            BatchNumMessages = BatchNumMessages,
            QueueBufferingMaxMessages = QueueBufferingMaxMessages,
            MessageTimeoutMs = MessageTimeoutMs
        };

        ApplyProperties(config);

        return config;
    }

    /// <summary>
    /// Maps configuration to Confluent <see cref="AdminClientConfig"/>.
    /// </summary>
    public AdminClientConfig ToAdminProperties()
    {
        var config = new AdminClientConfig(new Dictionary<string, string>(AdditionalConfiguration));

        ApplyProperties(config);

        return config;
    }
}
