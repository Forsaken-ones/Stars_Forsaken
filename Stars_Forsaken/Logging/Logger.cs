using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Stars_Forsaken.Logging
{
    public class Logger : ILogger
    {
        private readonly ConcurrentQueue<string> _logQueue = new ConcurrentQueue<string>();
        private readonly StreamWriter _writer;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly Task _loggingTask;

        public Logger(LoggerProvider loggerProvider)
        {
            _writer = loggerProvider.Writer;
            _loggingTask = Task.Run(() => ProcessLogQueue(_cancellationTokenSource.Token));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            string logMessage = formatter(state, exception);
            string logEntry;
            if (exception == null)
            {
                logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] - {logMessage}.";
            }
            else
            {
                logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] - {logMessage}: {exception?.Message}";
            }

            _logQueue.Enqueue(logEntry);
        }

        private void ProcessLogQueue(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_logQueue.TryDequeue(out string logEntry))
                {
                    try
                    {
                        Monitor.Enter(_writer);
                        _writer.WriteLine(logEntry);
                        _writer.Flush();
                    }
                    finally
                    {
                        Monitor.Exit(_writer);
                    }
                }
                else
                {
                    Thread.Sleep(100); // Adjust sleep time as needed
                }
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _loggingTask.Wait();
            _writer.Dispose();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }
    }
}
