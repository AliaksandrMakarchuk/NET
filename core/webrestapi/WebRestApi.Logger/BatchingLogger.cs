using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace WebRestApi.Logger
{
    public class BatchingLogger : ILogger
    {
        private readonly BatchingLoggerProvider _provider;
        private readonly string _category;
        private static object _lock = new Object();

        public BatchingLogger(BatchingLoggerProvider provider, string category)
        {
            _provider = provider;
            _category = category;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        // Write a log message
        public void Log<TState>(DateTimeOffset timestamp, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                return;
            }

            lock(_lock)
            {
                var builder = new StringBuilder();
                builder.Append(timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff zzz"));
                builder.Append(" [");
                builder.Append(logLevel.ToString());
                builder.Append("] ");
                builder.Append(_category);
                builder.Append(": ");
                builder.AppendLine(formatter(state, exception));

                if (exception != null)
                {
                    builder.AppendLine(exception.ToString());
                }

                _provider.AddMessage(timestamp, builder.ToString());
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Log(DateTimeOffset.Now, logLevel, eventId, state, exception, formatter);
        }
    }
}