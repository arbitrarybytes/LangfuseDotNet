using Xunit;
using Langfuse.Models.Observability;
using Langfuse.Extensions;

namespace Langfuse.Tests;

public class ObservationExtensionsTests
{
    [Fact]
    public void AsSpan_SetsTypeToSpan()
    {
        // Arrange
        var observation = new Observation();

        // Act
        var result = observation.AsSpan();

        // Assert
        Assert.Equal("SPAN", result.Type);
    }

    [Fact]
    public void AsGeneration_SetsTypeToGeneration()
    {
        // Arrange
        var observation = new Observation();

        // Act
        var result = observation.AsGeneration();

        // Assert
        Assert.Equal("GENERATION", result.Type);
    }

    [Fact]
    public void AsEvent_SetsTypeToEvent()
    {
        // Arrange
        var observation = new Observation();

        // Act
        var result = observation.AsEvent();

        // Assert
        Assert.Equal("EVENT", result.Type);
    }

    [Fact]
    public void WithTokens_SetsTokenCounts()
    {
        // Arrange
        var observation = new Observation();

        // Act
        var result = observation.WithTokens(10, 20);

        // Assert
        Assert.Equal(10, result.InputTokens);
        Assert.Equal(20, result.OutputTokens);
        Assert.Equal(30, result.TotalTokens);
    }

    [Fact]
    public void WithCost_SetsCostValues()
    {
        // Arrange
        var observation = new Observation();

        // Act
        var result = observation.WithCost(0.01m, 0.02m);

        // Assert
        Assert.Equal(0.01m, result.InputCost);
        Assert.Equal(0.02m, result.OutputCost);
        Assert.Equal(0.03m, result.TotalCost);
    }

    [Fact]
    public void WithModel_SetsModelInfo()
    {
        // Arrange
        var observation = new Observation();
        var modelParams = new Dictionary<string, object> { { "temperature", 0.7 } };

        // Act
        var result = observation.WithModel("gpt-4", modelParams);

        // Assert
        Assert.Equal("gpt-4", result.Model);
        Assert.NotNull(result.ModelParameters);
        Assert.Equal(0.7, result.ModelParameters["temperature"]);
    }

    [Fact]
    public void ChainedCalls_WorkCorrectly()
    {
        // Arrange & Act
        var observation = new Observation()
            .WithId("obs-123")
            .WithTraceId("trace-123")
            .AsGeneration()
            .WithName("Generate Response")
            .WithModel("gpt-4")
            .WithTokens(100, 200);

        // Assert
        Assert.Equal("obs-123", observation.Id);
        Assert.Equal("trace-123", observation.TraceId);
        Assert.Equal("GENERATION", observation.Type);
        Assert.Equal("Generate Response", observation.Name);
        Assert.Equal("gpt-4", observation.Model);
        Assert.Equal(300, observation.TotalTokens);
    }
}
