using System.Text.Json.Serialization;

namespace Langfuse.Models.Prompts;

/// <summary>
/// Represents a prompt version in Langfuse with complete prompt definition.
/// </summary>
public class PromptVersion
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("promptId")]
    public string? PromptId { get; set; }

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("prompt")]
    public string? Prompt { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; } // "text", "chat"

    [JsonPropertyName("chatMessages")]
    public List<ChatMessage>? ChatMessages { get; set; }

    [JsonPropertyName("config")]
    public Dictionary<string, object>? Config { get; set; }

    [JsonPropertyName("labels")]
    public List<string>? Labels { get; set; }

    [JsonPropertyName("variables")]
    public List<string>? Variables { get; set; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("createdBy")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("updatedBy")]
    public string? UpdatedBy { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("isDeprecated")]
    public bool IsDeprecated { get; set; }

    [JsonPropertyName("deprecated")]
    public bool? Deprecated { get; set; }

    [JsonPropertyName("externalReferenceId")]
    public string? ExternalReferenceId { get; set; }
}

/// <summary>
/// Represents a chat message in a prompt.
/// </summary>
public class ChatMessage
{
    [JsonPropertyName("role")]
    public string? Role { get; set; } // "system", "user", "assistant"

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("toolUseId")]
    public string? ToolUseId { get; set; }

    [JsonPropertyName("toolName")]
    public string? ToolName { get; set; }
}
