namespace SampleProject.BuildingBlocks.Messaging.Kafka.Abstractions;

/// <summary>
/// Constants.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Header name for the Message Id
    /// </summary>
    public const string MessageIdHeaderName = "message_id";

    /// <summary>
    /// Header name for the Message Type.
    /// </summary>
    public const string MessageTypeHeaderName = "message_type";

    /// <summary>
    /// Header name for the Trace Parent Id.
    /// </summary>
    public const string TraceParentHeaderName = "trace_parent";

    /// <summary>
    /// Header name for the Correlation Id.
    /// </summary>
    public const string CorrelationIdHeaderName = "correlation_id";

    /// <summary>
    /// Header name for the Tenant Id.
    /// </summary>
    public const string TenantIdHeaderName = "tenant_id";

    /// <summary>
    /// Header name for the Tenant Number.
    /// </summary>
    public const string TenantNumberHeaderName = "tenant_number";
}
