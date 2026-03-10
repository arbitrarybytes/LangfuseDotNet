using Langfuse.Models.Observability;
using Langfuse.Models.Common;

namespace Langfuse.Clients;

/// <summary>
/// Client for managing sessions in Langfuse.
/// </summary>
public interface ISessionClient
{
    /// <summary>
    /// Gets a session by ID.
    /// </summary>
    Task<Session?> GetAsync(string sessionId);

    /// <summary>
    /// Lists all sessions.
    /// </summary>
    Task<IEnumerable<Session>> ListAsync(int page = 1, int limit = 50);
}

/// <summary>
/// Implementation of the session client.
/// </summary>
internal class SessionClient : ISessionClient
{
    private readonly Http.LangfuseHttpClient _httpClient;

    public SessionClient(Http.LangfuseHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Session?> GetAsync(string sessionId)
    {
        return await _httpClient.GetAsync<Session>($"/api/public/sessions/{Uri.EscapeDataString(sessionId)}");
    }

    public async Task<IEnumerable<Session>> ListAsync(int page = 1, int limit = 50)
    {
        var endpoint = $"/api/public/sessions?page={page}&limit={limit}";
        var response = await _httpClient.GetAsync<PaginatedResponse<Session>>(endpoint);
        return response?.Data ?? Enumerable.Empty<Session>();
    }
}
