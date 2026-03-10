using Langfuse;
using Langfuse.Http;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for registering Langfuse client in dependency injection.
/// </summary>
public static class LangfuseServiceCollectionExtensions
{
    /// <summary>
    /// Adds Langfuse client to the service collection.
    /// </summary>
    public static IServiceCollection AddLangfuse(this IServiceCollection services, Action<LangfuseHttpClientOptions> configure)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var options = new LangfuseHttpClientOptions();
        configure(options);

        services.AddSingleton<ILangfuseClient>(sp => new LangfuseClient(options));

        return services;
    }

    /// <summary>
    /// Adds Langfuse client to the service collection using a builder.
    /// </summary>
    public static IServiceCollection AddLangfuse(this IServiceCollection services, Action<LangfuseClientBuilder> configure)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var builder = new LangfuseClientBuilder();
        configure(builder);
        var client = builder.Build();

        services.AddSingleton<ILangfuseClient>(client);

        return services;
    }

    /// <summary>
    /// Adds Langfuse client to the service collection using environment variables.
    /// Requires: LANGFUSE_API_KEY and LANGFUSE_SECRET_KEY
    /// </summary>
    public static IServiceCollection AddLangfuse(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var apiKey = Environment.GetEnvironmentVariable("LANGFUSE_API_KEY");
        var secretKey = Environment.GetEnvironmentVariable("LANGFUSE_SECRET_KEY");
        var baseUri = Environment.GetEnvironmentVariable("LANGFUSE_BASE_URI") ?? "https://api.langfuse.com";

        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("LANGFUSE_API_KEY environment variable is not set");
        }

        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("LANGFUSE_SECRET_KEY environment variable is not set");
        }

        var options = new LangfuseHttpClientOptions
        {
            ApiKey = apiKey,
            SecretKey = secretKey,
            BaseUri = new Uri(baseUri)
        };

        services.AddSingleton<ILangfuseClient>(sp => new LangfuseClient(options));

        return services;
    }
}
