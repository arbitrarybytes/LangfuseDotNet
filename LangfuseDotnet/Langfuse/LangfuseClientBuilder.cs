using System.Text.Json;
using Langfuse.Http;

namespace Langfuse;

/// <summary>
/// Builder for creating and configuring a Langfuse client with fluent API.
/// </summary>
public class LangfuseClientBuilder
{
    private readonly LangfuseHttpClientOptions _options = new();

    /// <summary>
    /// Sets the API key for authentication.
    /// </summary>
    public LangfuseClientBuilder WithApiKey(string apiKey)
    {
        _options.ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        return this;
    }

    /// <summary>
    /// Sets the secret key for authentication.
    /// </summary>
    public LangfuseClientBuilder WithSecretKey(string secretKey)
    {
        _options.SecretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
        return this;
    }

    /// <summary>
    /// Sets the base URI for the Langfuse API.
    /// </summary>
    public LangfuseClientBuilder WithBaseUri(string baseUri)
    {
        _options.BaseUri = new Uri(baseUri ?? throw new ArgumentNullException(nameof(baseUri)));
        return this;
    }

    /// <summary>
    /// Sets the base URI for the Langfuse API.
    /// </summary>
    public LangfuseClientBuilder WithBaseUri(Uri baseUri)
    {
        _options.BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        return this;
    }

    /// <summary>
    /// Sets the timeout for HTTP requests.
    /// </summary>
    public LangfuseClientBuilder WithTimeout(TimeSpan timeout)
    {
        _options.Timeout = timeout;
        return this;
    }

    /// <summary>
    /// Enables or disables automatic event batching.
    /// </summary>
    public LangfuseClientBuilder WithAutoBatch(bool enabled)
    {
        _options.AutoBatch = enabled;
        return this;
    }

    /// <summary>
    /// Sets the batch size for event batching.
    /// </summary>
    public LangfuseClientBuilder WithBatchSize(int batchSize)
    {
        if (batchSize <= 0)
        {
            throw new ArgumentException("Batch size must be greater than 0", nameof(batchSize));
        }

        _options.BatchSize = batchSize;
        return this;
    }

    /// <summary>
    /// Sets the batch interval for event flushing.
    /// </summary>
    public LangfuseClientBuilder WithBatchInterval(TimeSpan interval)
    {
        _options.BatchInterval = interval;
        return this;
    }

    /// <summary>
    /// Sets the maximum queue size.
    /// </summary>
    public LangfuseClientBuilder WithQueueMaxSize(int maxSize)
    {
        if (maxSize <= 0)
        {
            throw new ArgumentException("Queue max size must be greater than 0", nameof(maxSize));
        }

        _options.QueueMaxSize = maxSize;
        return this;
    }

    /// <summary>
    /// Sets the JSON serializer options.
    /// </summary>
    public LangfuseClientBuilder WithJsonOptions(JsonSerializerOptions options)
    {
        _options.JsonSerializerOptions = options;
        return this;
    }

    /// <summary>
    /// Builds and returns the configured Langfuse client.
    /// </summary>
    public LangfuseClient Build()
    {
        return new LangfuseClient(_options);
    }
}
