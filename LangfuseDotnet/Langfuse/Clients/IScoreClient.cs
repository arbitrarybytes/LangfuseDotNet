using Langfuse.Models.Observability;
using Langfuse.Models.Common;

namespace Langfuse.Clients;

/// <summary>
/// Client for managing scores and evaluations in Langfuse.
/// </summary>
public interface IScoreClient
{
    /// <summary>
    /// Creates a new score for a trace or observation.
    /// </summary>
    Task<Score?> CreateAsync(Score score);

    /// <summary>
    /// Gets a score by ID.
    /// </summary>
    Task<Score?> GetAsync(string scoreId);

    /// <summary>
    /// Updates an existing score.
    /// </summary>
    Task<Score?> UpdateAsync(string scoreId, Score score);

    /// <summary>
    /// Lists scores for a trace.
    /// </summary>
    Task<IEnumerable<Score>> ListByTraceAsync(string traceId, int page = 1, int limit = 50);

    /// <summary>
    /// Lists scores for an observation.
    /// </summary>
    Task<IEnumerable<Score>> ListByObservationAsync(string observationId, int page = 1, int limit = 50);

    /// <summary>
    /// Deletes a score.
    /// </summary>
    Task DeleteAsync(string scoreId);
}

/// <summary>
/// Implementation of the score client.
/// </summary>
internal class ScoreClient : IScoreClient
{
    private readonly Http.LangfuseHttpClient _httpClient;

    public ScoreClient(Http.LangfuseHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Score?> CreateAsync(Score score)
    {
        return await _httpClient.PostAsync<Score>("/api/public/scores", score);
    }

    public async Task<Score?> GetAsync(string scoreId)
    {
        return await _httpClient.GetAsync<Score>($"/api/public/scores/{Uri.EscapeDataString(scoreId)}");
    }

    public async Task<Score?> UpdateAsync(string scoreId, Score score)
    {
        return await _httpClient.PutAsync<Score>($"/api/public/scores/{Uri.EscapeDataString(scoreId)}", score);
    }

    public async Task<IEnumerable<Score>> ListByTraceAsync(string traceId, int page = 1, int limit = 50)
    {
        var endpoint = $"/api/public/traces/{Uri.EscapeDataString(traceId)}/scores?page={page}&limit={limit}";
        var response = await _httpClient.GetAsync<PaginatedResponse<Score>>(endpoint);
        return response?.Data ?? Enumerable.Empty<Score>();
    }

    public async Task<IEnumerable<Score>> ListByObservationAsync(string observationId, int page = 1, int limit = 50)
    {
        var endpoint = $"/api/public/observations/{Uri.EscapeDataString(observationId)}/scores?page={page}&limit={limit}";
        var response = await _httpClient.GetAsync<PaginatedResponse<Score>>(endpoint);
        return response?.Data ?? Enumerable.Empty<Score>();
    }

    public async Task DeleteAsync(string scoreId)
    {
        await _httpClient.DeleteAsync($"/api/public/scores/{Uri.EscapeDataString(scoreId)}");
    }
}
