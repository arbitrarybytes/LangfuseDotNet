using Langfuse.Models.Observability;
using Langfuse.Models.Common;

namespace Langfuse.Clients;

/// <summary>
/// Client for managing observations (spans, generations, events) in Langfuse.
/// </summary>
public interface IObservationClient
{
    /// <summary>
    /// Creates a new observation (span, generation, or event).
    /// </summary>
    Task<Observation?> CreateAsync(Observation observation);

    /// <summary>
    /// Gets an observation by ID.
    /// </summary>
    Task<Observation?> GetAsync(string observationId);

    /// <summary>
    /// Updates an existing observation.
    /// </summary>
    Task<Observation?> UpdateAsync(string observationId, Observation observation);

    /// <summary>
    /// Lists observations for a trace.
    /// </summary>
    Task<IEnumerable<Observation>> ListByTraceAsync(string traceId, int page = 1, int limit = 50);

    /// <summary>
    /// Deletes an observation.
    /// </summary>
    Task DeleteAsync(string observationId);
}

/// <summary>
/// Implementation of the observation client.
/// </summary>
internal class ObservationClient : IObservationClient
{
    private readonly Http.LangfuseHttpClient _httpClient;

    public ObservationClient(Http.LangfuseHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Observation?> CreateAsync(Observation observation)
    {
        return await _httpClient.PostAsync<Observation>("/api/public/observations", observation);
    }

    public async Task<Observation?> GetAsync(string observationId)
    {
        return await _httpClient.GetAsync<Observation>($"/api/public/observations/{Uri.EscapeDataString(observationId)}");
    }

    public async Task<Observation?> UpdateAsync(string observationId, Observation observation)
    {
        return await _httpClient.PutAsync<Observation>($"/api/public/observations/{Uri.EscapeDataString(observationId)}", observation);
    }

    public async Task<IEnumerable<Observation>> ListByTraceAsync(string traceId, int page = 1, int limit = 50)
    {
        var endpoint = $"/api/public/traces/{Uri.EscapeDataString(traceId)}/observations?page={page}&limit={limit}";
        var response = await _httpClient.GetAsync<PaginatedResponse<Observation>>(endpoint);
        return response?.Data ?? Enumerable.Empty<Observation>();
    }

    public async Task DeleteAsync(string observationId)
    {
        await _httpClient.DeleteAsync($"/api/public/observations/{Uri.EscapeDataString(observationId)}");
    }
}
