using Xunit;
using Langfuse.Events;

namespace Langfuse.Tests;

public class EventQueueTests
{
    [Fact]
    public void Enqueue_AddsEventToQueue()
    {
        // Arrange
        var queue = new EventQueue(100);
        var traceEvent = new TraceEvent { Trace = new Models.Observability.Trace { Id = "trace-1" } };

        // Act
        queue.Enqueue(traceEvent);

        // Assert
        Assert.Equal(1, queue.Count);
    }

    [Fact]
    public void TryDequeue_RemovesEventFromQueue()
    {
        // Arrange
        var queue = new EventQueue(100);
        var traceEvent = new TraceEvent { Trace = new Models.Observability.Trace { Id = "trace-1" } };
        queue.Enqueue(traceEvent);

        // Act
        var result = queue.TryDequeue(out var dequeued);

        // Assert
        Assert.True(result);
        Assert.NotNull(dequeued);
        Assert.Equal(0, queue.Count);
    }

    [Fact]
    public void TryDequeue_ReturnsFalseWhenEmpty()
    {
        // Arrange
        var queue = new EventQueue(100);

        // Act
        var result = queue.TryDequeue(out var dequeued);

        // Assert
        Assert.False(result);
        Assert.Null(dequeued);
    }

    [Fact]
    public void Enqueue_ThrowsWhenQueueIsFull()
    {
        // Arrange
        var queue = new EventQueue(1);
        var event1 = new TraceEvent { Trace = new Models.Observability.Trace { Id = "trace-1" } };
        var event2 = new TraceEvent { Trace = new Models.Observability.Trace { Id = "trace-2" } };
        queue.Enqueue(event1);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => queue.Enqueue(event2));
    }

    [Fact]
    public void Clear_RemovesAllEvents()
    {
        // Arrange
        var queue = new EventQueue(100);
        queue.Enqueue(new TraceEvent { Trace = new Models.Observability.Trace { Id = "trace-1" } });
        queue.Enqueue(new TraceEvent { Trace = new Models.Observability.Trace { Id = "trace-2" } });
        Assert.Equal(2, queue.Count);

        // Act
        queue.Clear();

        // Assert
        Assert.Equal(0, queue.Count);
    }
}
