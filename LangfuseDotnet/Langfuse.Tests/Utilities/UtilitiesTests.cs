using Xunit;
using Langfuse.Utilities;

namespace Langfuse.Tests;

public class UtilitiesTests
{
    [Fact]
    public void GenerateTraceId_ReturnsNonEmptyString()
    {
        // Act
        var id = IdGenerator.GenerateTraceId();

        // Assert
        Assert.NotEmpty(id);
    }

    [Fact]
    public void GenerateTraceId_ReturnsDifferentIds()
    {
        // Act
        var id1 = IdGenerator.GenerateTraceId();
        var id2 = IdGenerator.GenerateTraceId();

        // Assert
        Assert.NotEqual(id1, id2);
    }

    [Fact]
    public void CalculateDurationSeconds_ReturnsCorrectValue()
    {
        // Arrange
        var startTime = DateTime.UtcNow;
        var endTime = startTime.AddSeconds(5);

        // Act
        var duration = TimeHelper.CalculateDurationSeconds(startTime, endTime);

        // Assert
        Assert.Equal(5.0, duration, precision: 1);
    }

    [Fact]
    public void CalculateDurationMilliseconds_ReturnsCorrectValue()
    {
        // Arrange
        var startTime = DateTime.UtcNow;
        var endTime = startTime.AddMilliseconds(500);

        // Act
        var duration = TimeHelper.CalculateDurationMilliseconds(startTime, endTime);

        // Assert
        Assert.Equal(500.0, duration, precision: 10);
    }

    [Fact]
    public void ValidateRequired_ThrowsOnNullValue()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ValidationHelper.ValidateRequired(null, "testParam"));
    }

    [Fact]
    public void ValidateRequired_ThrowsOnEmptyString()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => ValidationHelper.ValidateRequired("", "testParam"));
    }

    [Fact]
    public void ValidateRequired_DoesNotThrowOnValidString()
    {
        // Act & Assert (no exception)
        ValidationHelper.ValidateRequired("valid-value", "testParam");
    }
}
