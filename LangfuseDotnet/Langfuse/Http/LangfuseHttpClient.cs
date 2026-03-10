using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Langfuse.Http;

/// <summary>
/// Base HTTP client for communicating with the Langfuse API.
/// </summary>
internal class LangfuseHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly LangfuseHttpClientOptions _options;
    private readonly JsonSerializerOptions _jsonOptions;

    public LangfuseHttpClient(HttpClient httpClient, LangfuseHttpClientOptions options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));

        _jsonOptions = options.JsonSerializerOptions ?? new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        ConfigureHttpClient();
    }

    private void ConfigureHttpClient()
    {
        _httpClient.BaseAddress = _options.BaseUri;
        _httpClient.Timeout = _options.Timeout;

        // Set up Basic Authentication header
        if (!string.IsNullOrEmpty(_options.ApiKey) && !string.IsNullOrEmpty(_options.SecretKey))
        {
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_options.ApiKey}:{_options.SecretKey}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }

        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "LangfuseDotNet/0.1.0");
    }

    /// <summary>
    /// Sends a GET request to the specified endpoint.
    /// </summary>
    public async Task<T?> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        return await DeserializeResponse<T>(response);
    }

    /// <summary>
    /// Sends a POST request with JSON content.
    /// </summary>
    public async Task<T?> PostAsync<T>(string endpoint, object? content = null)
    {
        var httpContent = content != null
            ? new StringContent(JsonSerializer.Serialize(content, _jsonOptions), Encoding.UTF8, "application/json")
            : null;

        var response = await _httpClient.PostAsync(endpoint, httpContent);
        return await DeserializeResponse<T>(response);
    }

    /// <summary>
    /// Sends a POST request without expecting a response.
    /// </summary>
    public async Task PostAsync(string endpoint, object? content = null)
    {
        var httpContent = content != null
            ? new StringContent(JsonSerializer.Serialize(content, _jsonOptions), Encoding.UTF8, "application/json")
            : null;

        var response = await _httpClient.PostAsync(endpoint, httpContent);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Sends a PUT request with JSON content.
    /// </summary>
    public async Task<T?> PutAsync<T>(string endpoint, object? content = null)
    {
        var httpContent = content != null
            ? new StringContent(JsonSerializer.Serialize(content, _jsonOptions), Encoding.UTF8, "application/json")
            : null;

        var response = await _httpClient.PutAsync(endpoint, httpContent);
        return await DeserializeResponse<T>(response);
    }

    /// <summary>
    /// Sends a PATCH request with JSON content.
    /// </summary>
    public async Task<T?> PatchAsync<T>(string endpoint, object? content = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, endpoint);
        if (content != null)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(content, _jsonOptions), Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        return await DeserializeResponse<T>(response);
    }

    /// <summary>
    /// Sends a DELETE request.
    /// </summary>
    public async Task DeleteAsync(string endpoint)
    {
        var response = await _httpClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
    }

    private async Task<T?> DeserializeResponse<T>(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();

        if (response.Content.Headers.ContentLength == 0)
        {
            return default;
        }

        var content = await response.Content.ReadAsStringAsync();
        return string.IsNullOrEmpty(content) ? default : JsonSerializer.Deserialize<T>(content, _jsonOptions);
    }
}
