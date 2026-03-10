using Xunit;
using Langfuse.Models.Observability;

namespace Langfuse.Tests;

public class DataModelTests
{
    [Fact]
    public void Trace_CanBeInstantiated()
    {
        // Arrange & Act
        var trace = new Trace
        {
            Id = "trace-1",
            Name = "Test Trace",
            UserId = "user-1"
        };

        // Assert
        Assert.Equal("trace-1", trace.Id);
        Assert.Equal("Test Trace", trace.Name);
        Assert.Equal("user-1", trace.UserId);
    }

    [Fact]
    public void Observation_CanBeInstantiated()
    {
        // Arrange & Act
        var observation = new Observation
        {
            Id = "obs-1",
            TraceId = "trace-1",
            Type = "GENERATION",
            Name = "Generate",
            Model = "gpt-4"
        };

        // Assert
        Assert.Equal("obs-1", observation.Id);
        Assert.Equal("trace-1", observation.TraceId);
        Assert.Equal("GENERATION", observation.Type);
    }

    [Fact]
    public void Score_CanBeInstantiated()
    {
        // Arrange & Act
        var score = new Score
        {
            Id = "score-1",
            TraceId = "trace-1",
            Name = "accuracy",
            Value = 0.95,
            DataType = "NUMERIC"
        };

        // Assert
        Assert.Equal("score-1", score.Id);
        Assert.Equal("trace-1", score.TraceId);
        Assert.Equal(0.95, score.Value);
    }

    [Fact]
    public void Session_CanBeInstantiated()
    {
        // Arrange & Act
        var session = new Session
        {
            Id = "session-1",
            UserId = "user-1",
            CreatedAt = DateTime.UtcNow
        };

        // Assert
        Assert.Equal("session-1", session.Id);
        Assert.Equal("user-1", session.UserId);
        Assert.NotNull(session.CreatedAt);
    }
}
