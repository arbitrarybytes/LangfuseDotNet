using System.Text;
using System.Text.Json;

namespace Langfuse.Http;

/// <summary>
/// Configuration for the Langfuse HTTP client.
/// </summary>
public class LangfuseHttpClientOptions
{
    /// <summary>
    /// Gets or sets the base URI for the Langfuse API.
    /// Default: https://api.langfuse.com
    /// </summary>
    public Uri? BaseUri { get; set; } = new Uri("https://api.langfuse.com");

    /// <summary>
    /// Gets or sets the API key for authentication.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the secret key for authentication.
    /// </summary>
    public string? SecretKey { get; set; }

    /// <summary>
    /// Gets or sets the timeout for HTTP requests.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Gets or sets whether to automatically batch events.
    /// </summary>
    public bool AutoBatch { get; set; } = true;

    /// <summary>
    /// Gets or sets the maximum batch size for events.
    /// </summary>
    public int BatchSize { get; set; } = 100;

    /// <summary>
    /// Gets or sets the interval for flushing batches.
    /// </summary>
    public TimeSpan BatchInterval { get; set; } = TimeSpan.FromSeconds(10);

    /// <summary>
    /// Gets or sets the maximum queue size before blocking.
    /// </summary>
    public int QueueMaxSize { get; set; } = 10000;

    /// <summary>
    /// Gets or sets the JSON serializer options.
    /// </summary>
    public JsonSerializerOptions? JsonSerializerOptions { get; set; }
}
