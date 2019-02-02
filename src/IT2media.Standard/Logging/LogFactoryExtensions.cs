using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using IT2media.Standard.Logging.NLogTargets;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.LayoutRenderers;

namespace IT2media.Standard.Logging
{
    public static class LogFactoryExtensions
    {
        #region observableLogger
        private static Dictionary<int, ObservableLoggerProvider> _observableLoggerProvider = new Dictionary<int, ObservableLoggerProvider>();
        /// <summary>
        /// adds an observable logger, don't use this in production by default
        /// </summary>
        public static void AddObservableLogger(this ILoggerFactory loggerFactory)
        {
            var obsL = loggerFactory.GetAndAttachObservableLoggerProvider();
            obsL.Enable();
        }

        private static ObservableLoggerProvider GetAndAttachObservableLoggerProvider(this ILoggerFactory loggerFactory)
        {
            // gets an observablelogger provider for a specific logger factory
            // attaches it, when not yet attached
            int hashCode = loggerFactory.GetHashCode();
            if (!_observableLoggerProvider.ContainsKey(hashCode))
            {
                _observableLoggerProvider[hashCode] = new ObservableLoggerProvider();
                loggerFactory.AddProvider(_observableLoggerProvider[hashCode]);
            }
            return _observableLoggerProvider[hashCode];
        }

        /// <summary>
        /// disables the observable logger (if any attached)
        /// otherwise it will be attached, and disabled.. so use wisely
        /// </summary>
        public static void DisableObservableLogger(this ILoggerFactory loggerFactory)
        {
            var obsL = loggerFactory.GetAndAttachObservableLoggerProvider();
            obsL.Disable();
        }

        /// <summary>
        /// gets the observable collection of log category names
        /// </summary>
        /// <param name="loggerFactory">the logge rfactory the observable logger is attached to</param>
        /// <returns></returns>
        public static ObservableCollection<string> GetObservableLoggerKeys(this ILoggerFactory loggerFactory)
        {
            var obsL = loggerFactory.GetAndAttachObservableLoggerProvider();
            return obsL.CategoryNames;
        }
        /// <summary>
        /// gets a observable collection of log history for a specified category
        /// </summary>
        /// <param name="loggerFactory">the logger factory</param>
        /// <param name="categoryName">the name of the category</param>
        /// <returns></returns>
        public static ObservableCollection<string> GetObservableLogHistory(this ILoggerFactory loggerFactory, string categoryName)
        {
            var obsL = loggerFactory.GetAndAttachObservableLoggerProvider();
            return obsL.GetLogHistory(categoryName);
        }

        #endregion

        #region NLOG
        /// <summary>
        /// adds NLog to the logger factory
        /// </summary>
        /// <param name="loggerFactory">logger factory</param>
        /// <param name="configFilePath">the path to the config (if none is given, NLog will search for it)</param>
        public static void AddNLog(this ILoggerFactory loggerFactory, string configFilePath = null)
        {
            // register target class
            ConfigurationItemFactory.Default.Targets.RegisterDefinition("AppCenter", typeof(AppCenterTarget));
            LogManager.Configuration = new XmlLoggingConfiguration(configFilePath);
            var options = new NLogProviderOptions();
            var nlogProvider = new NLogLoggerProvider(options);
            loggerFactory.AddProvider(nlogProvider);
        }

        /// <summary>
        /// replaces the current nlog config with a new one
        /// </summary>
        /// <param name="loggerFactory">logger factory</param>
        /// <param name="xmlDataReader">a Xml data reader, with the new xml-config for nlog</param>
        public static void ReplaceNLogConfig(this ILoggerFactory loggerFactory, XmlReader xmlDataReader)
        {
            LogManager.Configuration = new XmlLoggingConfiguration(xmlDataReader, "NLog.config");
        }


        #endregion

       
    }
}
