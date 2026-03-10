namespace Langfuse.Events;

/// <summary>
/// Interface for batch processor.
/// </summary>
public interface IBatchProcessor
{
    /// <summary>
    /// Adds an event to be batched.
    /// </summary>
    Task EnqueueAsync(LangfuseEvent @event);

    /// <summary>
    /// Flushes all pending batches.
    /// </summary>
    Task FlushAsync();

    /// <summary>
    /// Starts the batch processor.
    /// </summary>
    Task StartAsync();

    /// <summary>
    /// Stops the batch processor.
    /// </summary>
    Task StopAsync();
}

/// <summary>
/// Batch processor for handling event batching.
/// </summary>
public class BatchProcessor : IBatchProcessor, IDisposable
{
    private readonly IEventQueue _queue;
    private readonly int _batchSize;
    private readonly TimeSpan _batchInterval;
    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _processingTask;

    public BatchProcessor(IEventQueue queue, int batchSize = 100, TimeSpan? batchInterval = null)
    {
        _queue = queue ?? throw new ArgumentNullException(nameof(queue));
        _batchSize = batchSize;
        _batchInterval = batchInterval ?? TimeSpan.FromSeconds(10);
    }

    public async Task EnqueueAsync(LangfuseEvent @event)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        _queue.Enqueue(@event);

        // If queue reaches batch size, flush immediately
        if (_queue.Count >= _batchSize)
        {
            await FlushAsync();
        }
    }

    public async Task FlushAsync()
    {
        var batch = new List<LangfuseEvent>();

        while (_queue.TryDequeue(out var @event) && batch.Count < _batchSize)
        {
            if (@event != null)
            {
                batch.Add(@event);
            }
        }

        if (batch.Count > 0)
        {
            // Process batch - implementation would call API here
            await ProcessBatchAsync(batch);
        }
    }

    public async Task StartAsync()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _processingTask = ProcessingLoopAsync(_cancellationTokenSource.Token);
        await Task.CompletedTask;
    }

    public async Task StopAsync()
    {
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            if (_processingTask != null)
            {
                try
                {
                    await _processingTask;
                }
                catch (OperationCanceledException)
                {
                    // Expected when cancelling
                }
            }

            // Flush remaining events
            await FlushAsync();
        }
    }

    private async Task ProcessingLoopAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(_batchInterval, cancellationToken);
                await FlushAsync();
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }

    private async Task ProcessBatchAsync(List<LangfuseEvent> batch)
    {
        // Group events by endpoint
        var groupedByEndpoint = batch.GroupBy(e => e.Endpoint);

        foreach (var group in groupedByEndpoint)
        {
            // Future: Implement actual API call to send batch
            await Task.CompletedTask;
        }
    }

    public void Dispose()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}
