using Langfuse.Models.Observability;
using Langfuse.Models.Prompts;

namespace Langfuse.Extensions;

/// <summary>
/// Extension methods for fluent trace creation.
/// </summary>
public static class TraceExtensions
{
    /// <summary>
    /// Creates a new trace with a generated ID if not provided.
    /// </summary>
    public static Trace WithId(this Trace trace, string? id = null)
    {
        trace.Id ??= id ?? Guid.NewGuid().ToString();
        return trace;
    }

    /// <summary>
    /// Sets the trace name.
    /// </summary>
    public static Trace WithName(this Trace trace, string name)
    {
        trace.Name = name ?? throw new ArgumentNullException(nameof(name));
        return trace;
    }

    /// <summary>
    /// Sets the user ID for the trace.
    /// </summary>
    public static Trace WithUserId(this Trace trace, string userId)
    {
        trace.UserId = userId;
        return trace;
    }

    /// <summary>
    /// Sets the session ID for the trace.
    /// </summary>
    public static Trace WithSessionId(this Trace trace, string sessionId)
    {
        trace.SessionId = sessionId;
        return trace;
    }

    /// <summary>
    /// Adds tags to the trace.
    /// </summary>
    public static Trace WithTags(this Trace trace, params string[] tags)
    {
        trace.Tags ??= new List<string>();
        trace.Tags.AddRange(tags);
        return trace;
    }

    /// <summary>
    /// Sets metadata for the trace.
    /// </summary>
    public static Trace WithMetadata(this Trace trace, Dictionary<string, object> metadata)
    {
        trace.Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        return trace;
    }

    /// <summary>
    /// Adds a metadata entry to the trace.
    /// </summary>
    public static Trace AddMetadata(this Trace trace, string key, object value)
    {
        trace.Metadata ??= new Dictionary<string, object>();
        trace.Metadata[key] = value;
        return trace;
    }

    /// <summary>
    /// Sets the input for the trace.
    /// </summary>
    public static Trace WithInput(this Trace trace, object input)
    {
        trace.Input = input;
        return trace;
    }

    /// <summary>
    /// Sets the output for the trace.
    /// </summary>
    public static Trace WithOutput(this Trace trace, object output)
    {
        trace.Output = output;
        return trace;
    }
}

/// <summary>
/// Extension methods for fluent observation creation.
/// </summary>
public static class ObservationExtensions
{
    /// <summary>
    /// Creates a new observation with a generated ID if not provided.
    /// </summary>
    public static Observation WithId(this Observation observation, string? id = null)
    {
        observation.Id ??= id ?? Guid.NewGuid().ToString();
        return observation;
    }

    /// <summary>
    /// Sets the observation as a span.
    /// </summary>
    public static Observation AsSpan(this Observation observation)
    {
        observation.Type = "SPAN";
        return observation;
    }

    /// <summary>
    /// Sets the observation as a generation.
    /// </summary>
    public static Observation AsGeneration(this Observation observation)
    {
        observation.Type = "GENERATION";
        return observation;
    }

    /// <summary>
    /// Sets the observation as an event.
    /// </summary>
    public static Observation AsEvent(this Observation observation)
    {
        observation.Type = "EVENT";
        return observation;
    }

    /// <summary>
    /// Sets the name of the observation.
    /// </summary>
    public static Observation WithName(this Observation observation, string name)
    {
        observation.Name = name;
        return observation;
    }

    /// <summary>
    /// Sets the trace ID for the observation.
    /// </summary>
    public static Observation WithTraceId(this Observation observation, string traceId)
    {
        observation.TraceId = traceId ?? throw new ArgumentNullException(nameof(traceId));
        return observation;
    }

    /// <summary>
    /// Sets the parent observation ID.
    /// </summary>
    public static Observation WithParentObservationId(this Observation observation, string parentId)
    {
        observation.ParentObservationId = parentId;
        return observation;
    }

    /// <summary>
    /// Sets the input for the observation.
    /// </summary>
    public static Observation WithInput(this Observation observation, object input)
    {
        observation.Input = input;
        return observation;
    }

    /// <summary>
    /// Sets the output for the observation.
    /// </summary>
    public static Observation WithOutput(this Observation observation, object output)
    {
        observation.Output = output;
        return observation;
    }

    /// <summary>
    /// Sets the LLM model information.
    /// </summary>
    public static Observation WithModel(this Observation observation, string model, Dictionary<string, object>? parameters = null)
    {
        observation.Model = model;
        observation.ModelParameters = parameters;
        return observation;
    }

    /// <summary>
    /// Sets token counts for the observation.
    /// </summary>
    public static Observation WithTokens(this Observation observation, int? inputTokens, int? outputTokens)
    {
        observation.InputTokens = inputTokens;
        observation.OutputTokens = outputTokens;
        observation.TotalTokens = (inputTokens ?? 0) + (outputTokens ?? 0);
        return observation;
    }

    /// <summary>
    /// Sets costs for the observation.
    /// </summary>
    public static Observation WithCost(this Observation observation, decimal? inputCost, decimal? outputCost)
    {
        observation.InputCost = inputCost;
        observation.OutputCost = outputCost;
        observation.TotalCost = (inputCost ?? 0) + (outputCost ?? 0);
        return observation;
    }
}

/// <summary>
/// Extension methods for fluent score creation.
/// </summary>
public static class ScoreExtensions
{
    /// <summary>
    /// Creates a new score with a generated ID if not provided.
    /// </summary>
    public static Score WithId(this Score score, string? id = null)
    {
        score.Id ??= id ?? Guid.NewGuid().ToString();
        return score;
    }

    /// <summary>
    /// Sets the name of the score.
    /// </summary>
    public static Score WithName(this Score score, string name)
    {
        score.Name = name ?? throw new ArgumentNullException(nameof(name));
        return score;
    }

    /// <summary>
    /// Sets the numeric value of the score.
    /// </summary>
    public static Score WithValue(this Score score, double value)
    {
        score.Value = value;
        score.DataType = "NUMERIC";
        return score;
    }

    /// <summary>
    /// Sets the string value of the score.
    /// </summary>
    public static Score WithStringValue(this Score score, string value)
    {
        score.StringValue = value;
        score.DataType = "STRING";
        return score;
    }

    /// <summary>
    /// Sets the boolean value of the score.
    /// </summary>
    public static Score WithBooleanValue(this Score score, bool value)
    {
        score.BooleanValue = value;
        score.DataType = "BOOLEAN";
        return score;
    }

    /// <summary>
    /// Sets the trace ID for the score.
    /// </summary>
    public static Score WithTraceId(this Score score, string traceId)
    {
        score.TraceId = traceId ?? throw new ArgumentNullException(nameof(traceId));
        return score;
    }

    /// <summary>
    /// Sets the observation ID for the score.
    /// </summary>
    public static Score WithObservationId(this Score score, string observationId)
    {
        score.ObservationId = observationId;
        return score;
    }

    /// <summary>
    /// Sets the comment for the score.
    /// </summary>
    public static Score WithComment(this Score score, string comment)
    {
        score.Comment = comment;
        return score;
    }
}

/// <summary>
/// Extension methods for fluent prompt creation.
/// </summary>
public static class PromptExtensions
{
    /// <summary>
    /// Sets the name of the prompt.
    /// </summary>
    public static Prompt WithName(this Prompt prompt, string name)
    {
        prompt.Name = name ?? throw new ArgumentNullException(nameof(name));
        return prompt;
    }

    /// <summary>
    /// Sets the description of the prompt.
    /// </summary>
    public static Prompt WithDescription(this Prompt prompt, string description)
    {
        prompt.Description = description;
        return prompt;
    }

    /// <summary>
    /// Adds tags to the prompt.
    /// </summary>
    public static Prompt WithTags(this Prompt prompt, params string[] tags)
    {
        prompt.Tags ??= new List<string>();
        prompt.Tags.AddRange(tags);
        return prompt;
    }

    /// <summary>
    /// Sets metadata for the prompt.
    /// </summary>
    public static Prompt WithMetadata(this Prompt prompt, Dictionary<string, object> metadata)
    {
        prompt.Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        return prompt;
    }
}

/// <summary>
/// Extension methods for fluent prompt version creation.
/// </summary>
public static class PromptVersionExtensions
{
    /// <summary>
    /// Sets the prompt text content.
    /// </summary>
    public static PromptVersion WithPrompt(this PromptVersion version, string prompt)
    {
        version.Prompt = prompt;
        version.Type = "text";
        return version;
    }

    /// <summary>
    /// Sets the chat messages for the prompt.
    /// </summary>
    public static PromptVersion WithChatMessages(this PromptVersion version, List<ChatMessage> messages)
    {
        version.ChatMessages = messages ?? throw new ArgumentNullException(nameof(messages));
        version.Type = "chat";
        return version;
    }

    /// <summary>
    /// Adds a chat message to the prompt.
    /// </summary>
    public static PromptVersion AddChatMessage(this PromptVersion version, string role, string content)
    {
        version.ChatMessages ??= new List<ChatMessage>();
        version.ChatMessages.Add(new ChatMessage { Role = role, Content = content });
        version.Type = "chat";
        return version;
    }

    /// <summary>
    /// Sets configuration for the prompt.
    /// </summary>
    public static PromptVersion WithConfig(this PromptVersion version, Dictionary<string, object> config)
    {
        version.Config = config;
        return version;
    }

    /// <summary>
    /// Adds a label to the prompt version.
    /// </summary>
    public static PromptVersion WithLabel(this PromptVersion version, string label)
    {
        version.Labels ??= new List<string>();
        version.Labels.Add(label);
        return version;
    }

    /// <summary>
    /// Sets the description of the prompt version.
    /// </summary>
    public static PromptVersion WithDescription(this PromptVersion version, string description)
    {
        version.Description = description;
        return version;
    }
}
