using System.Text.Json.Serialization;

namespace Langfuse.Models.Prompts;

/// <summary>
/// Represents a prompt in Langfuse with versions and metadata.
/// </summary>
public class Prompt
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("latestVersion")]
    public int? LatestVersion { get; set; }

    [JsonPropertyName("latestPublishedVersion")]
    public int? LatestPublishedVersion { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("createdBy")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    [JsonPropertyName("versions")]
    public List<PromptVersion>? Versions { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    [JsonPropertyName("projectId")]
    public string? ProjectId { get; set; }
}
