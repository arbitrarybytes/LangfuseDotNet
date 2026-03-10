using Langfuse.Models.Observability;
using Langfuse.Models.Common;
using Langfuse.Models.Prompts;

namespace Langfuse.Clients;

/// <summary>
/// Client for managing traces in Langfuse.
/// </summary>
public interface ITraceClient
{
    /// <summary>
    /// Creates a new trace.
    /// </summary>
    Task<Trace?> CreateAsync(Trace trace);

    /// <summary>
    /// Gets a trace by ID.
    /// </summary>
    Task<Trace?> GetAsync(string traceId);

    /// <summary>
    /// Updates an existing trace.
    /// </summary>
    Task<Trace?> UpdateAsync(string traceId, Trace trace);

    /// <summary>
    /// Lists traces with optional filtering and pagination.
    /// </summary>
    Task<IEnumerable<Trace>> ListAsync(int page = 1, int limit = 50);
}

/// <summary>
/// Implementation of the trace client.
/// </summary>
internal class TraceClient : ITraceClient
{
    private readonly Http.LangfuseHttpClient _httpClient;

    public TraceClient(Http.LangfuseHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Trace?> CreateAsync(Trace trace)
    {
        return await _httpClient.PostAsync<Trace>("/api/public/traces", trace);
    }

    public async Task<Trace?> GetAsync(string traceId)
    {
        return await _httpClient.GetAsync<Trace>($"/api/public/traces/{Uri.EscapeDataString(traceId)}");
    }

    public async Task<Trace?> UpdateAsync(string traceId, Trace trace)
    {
        return await _httpClient.PutAsync<Trace>($"/api/public/traces/{Uri.EscapeDataString(traceId)}", trace);
    }

    public async Task<IEnumerable<Trace>> ListAsync(int page = 1, int limit = 50)
    {
        var endpoint = $"/api/public/traces?page={page}&limit={limit}";
        var response = await _httpClient.GetAsync<PaginatedResponse<Trace>>(endpoint);
        return response?.Data ?? Enumerable.Empty<Trace>();
    }
}
