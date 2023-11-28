using Confluent.Kafka;

namespace SampleProject.Utils.Messaging.Kafka.Abstractions;

/// <summary>
/// The Kafka Security Options.
/// </summary>
public class KafkaSecurityOptions
{
    /// <summary>
    /// Enable or Disable Security Options.
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// The Sasl Username.
    /// </summary>
    public string SaslUsername { get; set; }

    /// <summary>
    /// The Sasl Password.
    /// </summary>
    public string SaslPassword { get; set; }

    /// <summary>
    /// SecurityProtocol enum value.
    /// </summary>
    public SecurityProtocol SecurityProtocol { get; set; } = SecurityProtocol.SaslPlaintext;

    /// <summary>
    /// The SaslMechanism to use.
    /// </summary>
    public SaslMechanism SaslMechanism { get; set; } = SaslMechanism.Plain;

    /// <summary>
    /// Enable OpenSSl builtin certificate verification.
    /// default: true
    /// </summary>
    public bool EnableSslCertificateVerification { get; set; } = true;

    /// <summary>
    /// File or directory path to CA certificates for verifying the broker's key.
    /// </summary>
    public string SslCaLocation { get; set; }

    /// <summary>
    /// Path to client's public key (PEM) used for authentication.
    /// </summary>
    public string SslCertificateLocation { get; set; }

    /// <summary>
    /// Path to client's keystore (PKCS#12) used for authentication.
    /// </summary>
    public string SslKeystoreLocation { get; set; }

    /// <summary>
    /// Client's keystore (PKCS#12) password.
    /// </summary>
    public string SslKeystorePassword { get; set; }

    /// <summary>
    /// Path to client's private key (PEM) used for authentication.
    /// </summary>
    public string SslKeyLocation { get; set; }

    /// <summary>
    /// Private key passphrase.
    /// </summary>
    public string SslKeyPassword { get; set; }
}
