using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;

namespace IT2media.Standard.Logging
{
    internal class ObservableLoggerProvider :ILoggerProvider
    {
        private ConcurrentDictionary<string, ObservableLogger> _loggerDict = new ConcurrentDictionary<string, ObservableLogger>();
        // ReSharper disable once RedundantDefaultMemberInitializer
        private bool _isEnabled = false;
        public ObservableCollection<string> CategoryNames { get; } = new ObservableCollection<string>();

        public ILogger CreateLogger(string categoryName)
        {
            ObservableLogger res;
            // get logger from dict or create a new one
            if (!_loggerDict.TryGetValue(categoryName, out res))
            {
                res = new ObservableLogger();
                CategoryNames.Add(categoryName);
                _loggerDict.TryAdd(categoryName, res);
            }

            res.Enabled = _isEnabled;
            return res;
        }

        public ObservableCollection<string> GetLogHistory(string categoryName)
        {
            ObservableLogger res;
            if (_loggerDict.TryGetValue(categoryName, out res))
            {
                return res.LogHistory;
            }
            return new ObservableCollection<string>();
        }

        /// <summary>
        /// disables ALL observable loggers
        /// </summary>
        public void Disable()
        {
            _isEnabled = false;
            SetEnabledToLogger();
        }

        /// <summary>
        /// disables ALL observable loggers
        /// </summary>
        public void Enable()
        {
            _isEnabled = true;
            SetEnabledToLogger();
        }

        private void SetEnabledToLogger()
        {
            foreach (var logger in _loggerDict)
            {
                logger.Value.Enabled = _isEnabled;
            }
        }

        public void Dispose()
        {

        }

        // the implementation of the logger is not public, its not necessary
        class ObservableLogger : ILogger
        {
            public ObservableCollection<string> LogHistory { get;  } = new ObservableCollection<string>();

            public bool Enabled { get; set; }
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if (!IsEnabled(logLevel))
                {
                    return;
                }

                if (formatter != null)
                {
                    LogHistory.Add(formatter(state, exception));
                }
                else
                {
                    if (state != null)
                    {
                        LogHistory.Add($"{DateTime.Now:s} [{logLevel.ToString()}]: {state}");
                    }

                    if (exception != null)
                    {
                        LogHistory.Add($"{DateTime.Now:s} [{logLevel.ToString()}] Exception Details: {exception.ToString().PadLeft(512, ' ').Substring(0, 511)}");
                    }
                }

                //cleanup LogHistory
                if (LogHistory.Count > 9999)
                {
                    for (var i = 0; i < 1000; i++)
                    {
                        LogHistory.RemoveAt(0);
                    }
                }
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return Enabled; // this logger is for debug purposes, we log everything here
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }
    }
}
