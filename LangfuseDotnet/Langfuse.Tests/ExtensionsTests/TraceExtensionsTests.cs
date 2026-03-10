using Xunit;
using Langfuse.Models.Observability;
using Langfuse.Extensions;

namespace Langfuse.Tests;

public class TraceExtensionsTests
{
    [Fact]
    public void WithId_GeneratesIdWhenNull()
    {
        // Arrange
        var trace = new Trace();

        // Act
        var result = trace.WithId();

        // Assert
        Assert.NotNull(result.Id);
        Assert.NotEmpty(result.Id);
    }

    [Fact]
    public void WithId_UsesProvidedId()
    {
        // Arrange
        var trace = new Trace();
        var providedId = "test-id-123";

        // Act
        var result = trace.WithId(providedId);

        // Assert
        Assert.Equal(providedId, result.Id);
    }

    [Fact]
    public void WithName_SetsTraceName()
    {
        // Arrange
        var trace = new Trace();
        var name = "Test Trace";

        // Act
        var result = trace.WithName(name);

        // Assert
        Assert.Equal(name, result.Name);
    }

    [Fact]
    public void WithUserId_SetsUserId()
    {
        // Arrange
        var trace = new Trace();
        var userId = "user-123";

        // Act
        var result = trace.WithUserId(userId);

        // Assert
        Assert.Equal(userId, result.UserId);
    }

    [Fact]
    public void WithSessionId_SetsSessionId()
    {
        // Arrange
        var trace = new Trace();
        var sessionId = "session-123";

        // Act
        var result = trace.WithSessionId(sessionId);

        // Assert
        Assert.Equal(sessionId, result.SessionId);
    }

    [Fact]
    public void ChainedCalls_WorkCorrectly()
    {
        // Arrange & Act
        var trace = new Trace()
            .WithId("test-id")
            .WithName("Test Trace")
            .WithUserId("user-123")
            .WithSessionId("session-123")
            .WithTags("tag1", "tag2");

        // Assert
        Assert.Equal("test-id", trace.Id);
        Assert.Equal("Test Trace", trace.Name);
        Assert.Equal("user-123", trace.UserId);
        Assert.Equal("session-123", trace.SessionId);
        Assert.NotNull(trace.Tags);
        Assert.Contains("tag1", trace.Tags);
        Assert.Contains("tag2", trace.Tags);
    }
}
