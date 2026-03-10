using System.Text.Json.Serialization;

namespace Langfuse.Models.Observability;

/// <summary>
/// Represents a session in Langfuse - a collection of related traces over time.
/// </summary>
public class Session
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("externalSessionId")]
    public string? ExternalSessionId { get; set; }

    [JsonPropertyName("userId")]
    public string? UserId { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}
