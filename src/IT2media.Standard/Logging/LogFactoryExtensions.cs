using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;

namespace IT2media.Standard.Logging
{
    public static class LogFactoryExtensions
    {
        #region observableLogger
        private static ObservableLoggerProvider _observableLoggerProvider;
        /// <summary>
        /// adds a observable logger, don't use this in production by default
        /// </summary>
        public static void AddObservableLogger(this LoggerFactory loggerFactory)
        {
            if (_observableLoggerProvider == null) // dont add it twice
            {
                _observableLoggerProvider = new ObservableLoggerProvider();
                loggerFactory.AddProvider(_observableLoggerProvider);
            }
            _observableLoggerProvider.Enable();
        }

        /// <summary>
        /// removes a observable logger
        /// </summary>
        public static void DisableObservableLogger(this LoggerFactory loggerFactory)
        {
            if (_observableLoggerProvider != null) // dont add it twice
            {
                _observableLoggerProvider.Disable();
            }
        }

        public static ObservableCollection<string> GetObservableLoggerKeys()
        {
            return _observableLoggerProvider.CategoryNames;
        }
        public static ObservableCollection<string> GetObservableLogHistory(string categoryName)
        {
            return _observableLoggerProvider.GetLogHistory(categoryName);
        }

        #endregion

        #region NLOG

        public static void AddNLog(this ILoggerFactory loggerFactory, string configFilePath = null)
        {
            LogManager.Configuration = new XmlLoggingConfiguration(configFilePath);
            var options = new NLogProviderOptions();
            var nlogProvider = new NLogLoggerProvider(options);
            loggerFactory.AddProvider(nlogProvider);
        }

        public static void ReplaceNLogConfig(this ILoggerFactory loggerFactory, XmlReader xmlDataReader)
        {
            LogManager.Configuration = new XmlLoggingConfiguration(xmlDataReader, "NLog.config");
        }

        #endregion

       
    }
}
