using System.Text.Json.Serialization;

namespace Langfuse.Models.Observability;

/// <summary>
/// Represents a score for evaluating outputs in Langfuse.
/// </summary>
public class Score
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("traceId")]
    public string? TraceId { get; set; }

    [JsonPropertyName("observationId")]
    public string? ObservationId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public double? Value { get; set; }

    [JsonPropertyName("stringValue")]
    public string? StringValue { get; set; }

    [JsonPropertyName("booleanValue")]
    public bool? BooleanValue { get; set; }

    [JsonPropertyName("dataType")]
    public string? DataType { get; set; } // "NUMERIC", "STRING", "BOOLEAN", "CATEGORICAL"

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime? Timestamp { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("authorUserId")]
    public string? AuthorUserId { get; set; }

    [JsonPropertyName("configId")]
    public string? ConfigId { get; set; }
}
