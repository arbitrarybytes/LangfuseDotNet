namespace Langfuse.Events;

/// <summary>
/// Represents an event to be sent to Langfuse.
/// </summary>
public abstract class LangfuseEvent
{
    /// <summary>
    /// Gets the unique identifier for the event.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets the timestamp when the event was created.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the endpoint path for this event.
    /// </summary>
    public abstract string Endpoint { get; }

    /// <summary>
    /// Gets the event payload.
    /// </summary>
    public abstract object GetPayload();
}

/// <summary>
/// Represents a trace event.
/// </summary>
public class TraceEvent : LangfuseEvent
{
    public override string Endpoint => "/api/public/traces";
    public Models.Observability.Trace? Trace { get; set; }
    public override object GetPayload() => Trace ?? new object();
}

/// <summary>
/// Represents an observation event.
/// </summary>
public class ObservationEvent : LangfuseEvent
{
    public override string Endpoint => "/api/public/observations";
    public Models.Observability.Observation? Observation { get; set; }
    public override object GetPayload() => Observation ?? new object();
}

/// <summary>
/// Represents a score event.
/// </summary>
public class ScoreEvent : LangfuseEvent
{
    public override string Endpoint => "/api/public/scores";
    public Models.Observability.Score? Score { get; set; }
    public override object GetPayload() => Score ?? new object();
}
