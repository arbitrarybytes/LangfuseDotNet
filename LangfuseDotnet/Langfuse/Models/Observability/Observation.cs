using System.Text.Json.Serialization;

namespace Langfuse.Models.Observability;

/// <summary>
/// Represents an observation (span, generation, or event) within a trace.
/// </summary>
public class Observation
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("traceId")]
    public string? TraceId { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; } // "SPAN", "GENERATION", "EVENT"

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("startTime")]
    public DateTime? StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public DateTime? EndTime { get; set; }

    [JsonPropertyName("input")]
    public object? Input { get; set; }

    [JsonPropertyName("output")]
    public object? Output { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    [JsonPropertyName("level")]
    public string? Level { get; set; } // "DEBUG", "INFO", "WARNING", "ERROR"

    [JsonPropertyName("statusMessage")]
    public string? StatusMessage { get; set; }

    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("modelParameters")]
    public Dictionary<string, object>? ModelParameters { get; set; }

    [JsonPropertyName("inputCost")]
    public decimal? InputCost { get; set; }

    [JsonPropertyName("outputCost")]
    public decimal? OutputCost { get; set; }

    [JsonPropertyName("totalCost")]
    public decimal? TotalCost { get; set; }

    [JsonPropertyName("inputTokens")]
    public int? InputTokens { get; set; }

    [JsonPropertyName("outputTokens")]
    public int? OutputTokens { get; set; }

    [JsonPropertyName("totalTokens")]
    public int? TotalTokens { get; set; }

    [JsonPropertyName("completionStartTime")]
    public DateTime? CompletionStartTime { get; set; }

    [JsonPropertyName("promptId")]
    public string? PromptId { get; set; }

    [JsonPropertyName("promptVersion")]
    public int? PromptVersion { get; set; }

    [JsonPropertyName("version")]
    public int? Version { get; set; }

    [JsonPropertyName("parentObservationId")]
    public string? ParentObservationId { get; set; }
}
