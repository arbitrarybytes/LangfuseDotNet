using System.Collections.Concurrent;

namespace Langfuse.Events;

/// <summary>
/// Interface for event queue.
/// </summary>
public interface IEventQueue
{
    /// <summary>
    /// Enqueues an event.
    /// </summary>
    void Enqueue(LangfuseEvent @event);

    /// <summary>
    /// Dequeues an event.
    /// </summary>
    bool TryDequeue(out LangfuseEvent? @event);

    /// <summary>
    /// Gets the current queue size.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Clears the queue.
    /// </summary>
    void Clear();
}

/// <summary>
/// Event queue implementation using ConcurrentQueue.
/// </summary>
public class EventQueue : IEventQueue
{
    private readonly ConcurrentQueue<LangfuseEvent> _queue = new();
    private readonly int _maxSize;

    public int Count => _queue.Count;

    public EventQueue(int maxSize = 10000)
    {
        _maxSize = maxSize;
    }

    public void Enqueue(LangfuseEvent @event)
    {
        if (_queue.Count >= _maxSize)
        {
            throw new InvalidOperationException($"Event queue is full (max size: {_maxSize})");
        }

        _queue.Enqueue(@event);
    }

    public bool TryDequeue(out LangfuseEvent? @event)
    {
        return _queue.TryDequeue(out @event);
    }

    public void Clear()
    {
        while (_queue.TryDequeue(out _)) { }
    }
}
