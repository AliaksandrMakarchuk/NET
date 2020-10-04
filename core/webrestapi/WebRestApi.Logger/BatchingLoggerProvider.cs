using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebRestApi.Logger
{
    public abstract class BatchingLoggerProvider : ILoggerProvider
    {
        // private readonly List<LogMessage> _currentBatch = new List<LogMessage>();
        // private readonly TimeSpan _interval;
        // private BlockingCollection<LogMessage> _messageQueue = new BlockingCollection<LogMessage>(new ConcurrentQueue<LogMessage>());
        // private Task _outputTask;
        // private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        // protected BatchingLoggerProvider(IOptions<BatchingLoggerOptions> options)
        // {
        //     _interval = options.Value.FlushPeriod;
        //     _outputTask = Task.Factory.StartNew<Task>(
        //         ProcessLogQueue,
        //         null,
        //         TaskCreationOptions.LongRunning);
        // }

        // protected abstract Task WriteMessagesAsync(IEnumerable<LogMessage> messages, CancellationToken token);

        // private async Task ProcessLogQueue(object state)
        // {
        //     while (!_cancellationTokenSource.IsCancellationRequested)
        //     {
        //         while(_messageQueue.TryTake(out var message))
        //         {
        //             _currentBatch.Add(message);
        //         }

        //         // Write the current batch out
        //         await WriteMessagesAsync(_currentBatch, _cancellationTokenSource.Token);
        //         _currentBatch.Clear();

        //         // Wait before writing the next batch
        //         await Task.Delay(_interval, _cancellationTokenSource.Token);
        //     }
        // }

        // Add a message to the concurrent queue
        internal void AddMessage(DateTimeOffset timestamp, string message)
        {
            // if (!_messageQueue.IsAddingCompleted)
            // {
            //     _messageQueue.Add(new LogMessage { Message = message, Timestamp = timestamp }, _cancellationTokenSource.Token);
            // }
        }

        public ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
