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
        private static Dictionary<int, ObservableLoggerProvider> _observableLoggerProvider = new Dictionary<int, ObservableLoggerProvider>();
        /// <summary>
        /// adds a observable logger, don't use this in production by default
        /// </summary>
        public static void AddObservableLogger(this LoggerFactory loggerFactory)
        {
            var obsL = loggerFactory.GetAndAttachObservableLoggerProvider();
            obsL.Enable();
        }

        private static ObservableLoggerProvider GetAndAttachObservableLoggerProvider(this LoggerFactory loggerFactory)
        {
            int hashCode = loggerFactory.GetHashCode();
            if (!_observableLoggerProvider.ContainsKey(hashCode))
            {
                _observableLoggerProvider[hashCode] = new ObservableLoggerProvider();
                loggerFactory.AddProvider(_observableLoggerProvider[hashCode]);
            }
            return _observableLoggerProvider[hashCode];
        }

        /// <summary>
        /// removes a observable logger
        /// </summary>
        public static void DisableObservableLogger(this LoggerFactory loggerFactory)
        {
            var obsL = loggerFactory.GetAndAttachObservableLoggerProvider();
            obsL.Disable();
        }

        public static ObservableCollection<string> GetObservableLoggerKeys(this LoggerFactory loggerFactory)
        {
            var obsL = loggerFactory.GetAndAttachObservableLoggerProvider();
            return obsL.CategoryNames;
        }
        public static ObservableCollection<string> GetObservableLogHistory(this LoggerFactory loggerFactory, string categoryName)
        {
            var obsL = loggerFactory.GetAndAttachObservableLoggerProvider();
            return obsL.GetLogHistory(categoryName);
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
