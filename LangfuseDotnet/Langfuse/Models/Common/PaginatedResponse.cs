using System.Text.Json.Serialization;

namespace Langfuse.Models.Common;

/// <summary>
/// Represents a list response from the Langfuse API.
/// </summary>
public class PaginatedResponse<T>
{
    [JsonPropertyName("data")]
    public List<T>? Data { get; set; }

    [JsonPropertyName("meta")]
    public PaginationMeta? Meta { get; set; }
}

/// <summary>
/// Pagination metadata for API responses.
/// </summary>
public class PaginationMeta
{
    [JsonPropertyName("page")]
    public int? Page { get; set; }

    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    [JsonPropertyName("totalItems")]
    public int? TotalItems { get; set; }

    [JsonPropertyName("totalPages")]
    public int? TotalPages { get; set; }
}
