namespace Langfuse.Utilities;

/// <summary>
/// Utility class for generating unique identifiers.
/// </summary>
public static class IdGenerator
{
    /// <summary>
    /// Generates a unique trace ID.
    /// </summary>
    public static string GenerateTraceId() => Guid.NewGuid().ToString();

    /// <summary>
    /// Generates a unique observation ID.
    /// </summary>
    public static string GenerateObservationId() => Guid.NewGuid().ToString();

    /// <summary>
    /// Generates a unique session ID.
    /// </summary>
    public static string GenerateSessionId() => Guid.NewGuid().ToString();

    /// <summary>
    /// Generates a unique score ID.
    /// </summary>
    public static string GenerateScoreId() => Guid.NewGuid().ToString();
}

/// <summary>
/// Utility class for time conversions.
/// </summary>
public static class TimeHelper
{
    /// <summary>
    /// Gets the current UTC timestamp.
    /// </summary>
    public static DateTime GetCurrentUtcTimestamp() => DateTime.UtcNow;

    /// <summary>
    /// Calculates duration between two timestamps in seconds.
    /// </summary>
    public static double CalculateDurationSeconds(DateTime startTime, DateTime endTime)
    {
        return (endTime - startTime).TotalSeconds;
    }

    /// <summary>
    /// Calculates duration between two timestamps in milliseconds.
    /// </summary>
    public static double CalculateDurationMilliseconds(DateTime startTime, DateTime endTime)
    {
        return (endTime - startTime).TotalMilliseconds;
    }
}

/// <summary>
/// Utility class for common validation operations.
/// </summary>
public static class ValidationHelper
{
    /// <summary>
    /// Validates that a required string is not null or empty.
    /// </summary>
    public static void ValidateRequired(string? value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{parameterName} is required", parameterName);
        }
    }

    /// <summary>
    /// Validates that a trace ID is valid.
    /// </summary>
    public static void ValidateTraceId(string? traceId)
    {
        ValidateRequired(traceId, nameof(traceId));
    }

    /// <summary>
    /// Validates that an observation ID is valid.
    /// </summary>
    public static void ValidateObservationId(string? observationId)
    {
        ValidateRequired(observationId, nameof(observationId));
    }
}
