using Langfuse.Clients;
using Langfuse.Http;

namespace Langfuse;

/// <summary>
/// Main Langfuse client for interacting with the Langfuse API.
/// </summary>
public interface ILangfuseClient
{
    /// <summary>
    /// Gets the trace client.
    /// </summary>
    ITraceClient Traces { get; }

    /// <summary>
    /// Gets the observation client.
    /// </summary>
    IObservationClient Observations { get; }

    /// <summary>
    /// Gets the score client.
    /// </summary>
    IScoreClient Scores { get; }

    /// <summary>
    /// Gets the session client.
    /// </summary>
    ISessionClient Sessions { get; }

    /// <summary>
    /// Gets the prompt client.
    /// </summary>
    IPromptClient Prompts { get; }

    /// <summary>
    /// Flushes any pending events.
    /// </summary>
    Task FlushAsync();
}

/// <summary>
/// Implementation of the Langfuse client.
/// </summary>
public class LangfuseClient : ILangfuseClient
{
    private readonly HttpClient _httpClient;
    private readonly LangfuseHttpClientOptions _options;
    private readonly LangfuseHttpClient _apiClient;
    private readonly ITraceClient _traceClient;
    private readonly IObservationClient _observationClient;
    private readonly IScoreClient _scoreClient;
    private readonly ISessionClient _sessionClient;
    private readonly IPromptClient _promptClient;

    public ITraceClient Traces => _traceClient;
    public IObservationClient Observations => _observationClient;
    public IScoreClient Scores => _scoreClient;
    public ISessionClient Sessions => _sessionClient;
    public IPromptClient Prompts => _promptClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="LangfuseClient"/> class.
    /// </summary>
    public LangfuseClient(LangfuseHttpClientOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));

        if (string.IsNullOrEmpty(options.ApiKey))
        {
            throw new ArgumentException("ApiKey is required", nameof(options));
        }

        if (string.IsNullOrEmpty(options.SecretKey))
        {
            throw new ArgumentException("SecretKey is required", nameof(options));
        }

        _httpClient = new HttpClient();
        _apiClient = new LangfuseHttpClient(_httpClient, options);

        _traceClient = new TraceClient(_apiClient);
        _observationClient = new ObservationClient(_apiClient);
        _scoreClient = new ScoreClient(_apiClient);
        _sessionClient = new SessionClient(_apiClient);
        _promptClient = new PromptClient(_apiClient);
    }

    /// <summary>
    /// Flushes any pending events.
    /// </summary>
    public async Task FlushAsync()
    {
        // Future: Implement event queue flushing
        await Task.CompletedTask;
    }

    /// <summary>
    /// Disposes the client resources.
    /// </summary>
    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
