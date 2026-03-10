# Langfuse .NET SDK

A comprehensive .NET SDK for [Langfuse](https://langfuse.com) - an open-source LLM engineering platform for tracing, prompt management, and evaluation of LLM applications.

## Features

### 🔍 Observability
- **Trace Management**: Log and track LLM application flows
- **Observations**: Record spans, generations, and events
- **Sessions**: Track multi-step conversations and agentic workflows
- **User Tracking**: Monitor costs and usage per user
- **Scoring & Evaluation**: Record evaluation scores and metrics
- **Token & Cost Tracking**: Automatically track token usage and costs

### 📝 Prompt Management
- **Version Control**: Create and manage multiple prompt versions
- **Deployment**: Deploy prompts to production with labels
- **Composability**: Build complex prompts from simpler components
- **Variables & Placeholders**: Use dynamic variables in prompts
- **Configuration**: Store LLM parameters alongside prompts

### 📊 Evaluation
- **Score Recording**: Log evaluation scores via API
- **Metrics Tracking**: Monitor performance metrics over time
- **Custom Evaluations**: Support numeric, string, boolean, and categorical values

## Installation

Install the NuGet package:

```bash
dotnet add package Langfuse
```

## Quick Start

### Basic Setup

#### Using Builder Pattern
```csharp
using Langfuse;

var client = new LangfuseClientBuilder()
    .WithApiKey("your-api-key")
    .WithSecretKey("your-secret-key")
    .WithBaseUri("https://api.langfuse.com")
    .Build();
```

#### Using Dependency Injection
```csharp
using Microsoft.Extensions.DependencyInjection;
using Langfuse;

var services = new ServiceCollection();

// Using builder
services.AddLangfuse(builder =>
    builder
        .WithApiKey("your-api-key")
        .WithSecretKey("your-secret-key")
);

// Or using environment variables (LANGFUSE_API_KEY, LANGFUSE_SECRET_KEY)
services.AddLangfuse();

var provider = services.BuildServiceProvider();
var client = provider.GetRequiredService<ILangfuseClient>();
```

### Creating Traces

```csharp
var trace = new Trace()
    .WithId("trace-1")
    .WithName("LLM Application Flow")
    .WithUserId("user-123")
    .WithSessionId("session-123")
    .WithInput(new { question = "What is AI?" })
    .WithOutput(new { answer = "Artificial Intelligence is..." })
    .WithTags("production", "llm");

var createdTrace = await client.Traces.CreateAsync(trace);
```

### Recording Observations

```csharp
var observation = new Observation()
    .WithId("obs-1")
    .WithTraceId("trace-1")
    .AsGeneration()
    .WithName("LLM Call")
    .WithModel("gpt-4")
    .WithInput(new { prompt = "..." })
    .WithOutput(new { text = "..." })
    .WithTokens(inputTokens: 100, outputTokens: 200)
    .WithCost(inputCost: 0.001m, outputCost: 0.002m);

var createdObs = await client.Observations.CreateAsync(observation);
```

### Logging Evaluation Scores

```csharp
var score = new Score()
    .WithId("score-1")
    .WithTraceId("trace-1")
    .WithName("accuracy")
    .WithValue(0.95);

await client.Scores.CreateAsync(score);
```

### Managing Prompts

#### Create a Prompt
```csharp
var prompt = new Prompt()
    .WithName("my-prompt")
    .WithDescription("My first prompt")
    .WithTags("important");

await client.Prompts.CreateAsync(prompt);
```

#### Create a Prompt Version
```csharp
var version = new PromptVersion()
    .WithPrompt("Hello, {{name}}!")
    .WithLabel("production")
    .WithDescription("Version 1 - Simple greeting");

var createdVersion = await client.Prompts.CreateVersionAsync("my-prompt", version);
```

#### Get a Prompt Version
```csharp
// Get latest version with a label
var version = await client.Prompts.GetVersionAsync("my-prompt", label: "production");

// Get specific version
var specificVersion = await client.Prompts.GetVersionAsync("my-prompt", version: 1);
```

#### Chat Prompts
```csharp
var chatVersion = new PromptVersion()
    .AddChatMessage("system", "You are a helpful assistant.")
    .AddChatMessage("user", "What is {{topic}}?")
    .WithLabel("v1");

await client.Prompts.CreateVersionAsync("chat-prompt", chatVersion);
```

## Architecture

### Models

The SDK includes comprehensive data models for:
- **Observability**: `Trace`, `Observation`, `Score`, `Session`, `LangfuseUser`
- **Prompts**: `Prompt`, `PromptVersion`, `ChatMessage`
- **Common**: `PaginatedResponse` for API responses

### Clients

- `ITraceClient`: Manage traces
- `IObservationClient`: Manage observations (spans, generations, events)
- `IScoreClient`: Manage scores and evaluations
- `ISessionClient`: Manage sessions
- `IPromptClient`: Manage prompts and versions
- `ILangfuseClient`: Main interface combining all clients

### Extension Methods

Fluent extension methods available for all main models:

```csharp
trace
    .WithId("id")
    .WithName("name")
    .WithUserId("user-id")
    .WithMetadata(new { key = "value" })
    .WithTags("tag1", "tag2");

observation
    .AsGeneration()
    .WithModel("gpt-4")
    .WithTokens(100, 200)
    .WithCost(0.01m, 0.02m);
```

### Batch Processing & Queuing

The SDK includes infrastructure for batching events:

```csharp
var queue = new EventQueue(maxSize: 10000);
var processor = new BatchProcessor(queue, batchSize: 100, batchInterval: TimeSpan.FromSeconds(10));

await processor.StartAsync();
// Add events...
await processor.FlushAsync();
await processor.StopAsync();
```

### Utilities

- `IdGenerator`: Generate unique IDs for traces, observations, etc.
- `TimeHelper`: Calculate durations between timestamps
- `ValidationHelper`: Validate required parameters

## Configuration

### HTTP Client Options

```csharp
new LangfuseClientBuilder()
    .WithApiKey("key")
    .WithSecretKey("secret")
    .WithBaseUri("https://api.langfuse.com")
    .WithTimeout(TimeSpan.FromSeconds(30))
    .WithAutoBatch(true)
    .WithBatchSize(100)
    .WithBatchInterval(TimeSpan.FromSeconds(10))
    .WithQueueMaxSize(10000)
    .Build();
```

### Environment Variables

- `LANGFUSE_API_KEY`: Your API key
- `LANGFUSE_SECRET_KEY`: Your secret key
- `LANGFUSE_BASE_URI`: API base URI (optional, defaults to https://api.langfuse.com)

## Testing

The project includes comprehensive unit tests using xUnit:

```bash
dotnet test
```

Tests cover:
- Extension methods and fluent API
- Data models
- Event queue and batch processing
- Utilities and helpers

## API Reference

For complete API documentation, visit:
- [Langfuse Documentation](https://langfuse.com/docs)
- [API Reference](https://api.reference.langfuse.com/)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

- [Langfuse Documentation](https://langfuse.com/docs)
- [GitHub Issues](https://github.com/arbitrarybytes/LangfuseDotNet/issues)
- [Langfuse Support](https://langfuse.com/support)

## Changelog

### v0.1.0 (Initial Release)
- Core observability API client (traces, observations, scores, sessions)
- Prompt management API client (prompts, versions, labels)
- Fluent extension methods for all main models
- Event queuing and batch processing infrastructure
- Dependency injection support
- Comprehensive unit tests
- Full XML documentation
