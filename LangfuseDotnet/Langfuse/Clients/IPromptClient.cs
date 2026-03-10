using Langfuse.Models.Prompts;
using Langfuse.Models.Common;

namespace Langfuse.Clients;

/// <summary>
/// Client for managing prompts in Langfuse.
/// </summary>
public interface IPromptClient
{
    /// <summary>
    /// Creates a new prompt.
    /// </summary>
    Task<Prompt?> CreateAsync(Prompt prompt);

    /// <summary>
    /// Gets a prompt by name.
    /// </summary>
    Task<Prompt?> GetAsync(string name);

    /// <summary>
    /// Gets a specific version of a prompt.
    /// </summary>
    Task<PromptVersion?> GetVersionAsync(string name, int? version = null, string? label = null);

    /// <summary>
    /// Lists all prompts.
    /// </summary>
    Task<IEnumerable<Prompt>> ListAsync(int page = 1, int limit = 50);

    /// <summary>
    /// Creates a new version of an existing prompt.
    /// </summary>
    Task<PromptVersion?> CreateVersionAsync(string promptName, PromptVersion version);

    /// <summary>
    /// Updates a prompt.
    /// </summary>
    Task<Prompt?> UpdateAsync(string promptName, Prompt prompt);

    /// <summary>
    /// Deletes a prompt and all its versions.
    /// </summary>
    Task DeleteAsync(string promptName);
}

/// <summary>
/// Implementation of the prompt client.
/// </summary>
internal class PromptClient : IPromptClient
{
    private readonly Http.LangfuseHttpClient _httpClient;

    public PromptClient(Http.LangfuseHttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Prompt?> CreateAsync(Prompt prompt)
    {
        return await _httpClient.PostAsync<Prompt>("/api/public/prompts", prompt);
    }

    public async Task<Prompt?> GetAsync(string name)
    {
        return await _httpClient.GetAsync<Prompt>($"/api/public/prompts/{Uri.EscapeDataString(name)}");
    }

    public async Task<PromptVersion?> GetVersionAsync(string name, int? version = null, string? label = null)
    {
        var endpoint = $"/api/public/prompts/{Uri.EscapeDataString(name)}";
        
        if (version.HasValue)
        {
            endpoint += $"/versions/{version.Value}";
        }
        else if (!string.IsNullOrEmpty(label))
        {
            endpoint += $"?label={Uri.EscapeDataString(label)}";
        }

        return await _httpClient.GetAsync<PromptVersion>(endpoint);
    }

    public async Task<IEnumerable<Prompt>> ListAsync(int page = 1, int limit = 50)
    {
        var endpoint = $"/api/public/prompts?page={page}&limit={limit}";
        var response = await _httpClient.GetAsync<PaginatedResponse<Prompt>>(endpoint);
        return response?.Data ?? Enumerable.Empty<Prompt>();
    }

    public async Task<PromptVersion?> CreateVersionAsync(string promptName, PromptVersion version)
    {
        return await _httpClient.PostAsync<PromptVersion>(
            $"/api/public/prompts/{Uri.EscapeDataString(promptName)}/versions", 
            version);
    }

    public async Task<Prompt?> UpdateAsync(string promptName, Prompt prompt)
    {
        return await _httpClient.PutAsync<Prompt>(
            $"/api/public/prompts/{Uri.EscapeDataString(promptName)}", 
            prompt);
    }

    public async Task DeleteAsync(string promptName)
    {
        await _httpClient.DeleteAsync($"/api/public/prompts/{Uri.EscapeDataString(promptName)}");
    }
}
