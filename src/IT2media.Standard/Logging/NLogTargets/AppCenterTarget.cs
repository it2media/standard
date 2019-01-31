using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using NLog;
using NLog.Config;
using NLog.Targets;
using LogLevel = Microsoft.AppCenter.LogLevel;

namespace IT2media.Standard.Logging.NLogTargets
{
    [Target("AppCenter")]

    public sealed class AppCenterTarget : TargetWithLayout
    {
        public AppCenterTarget()
        {
            
        }

        [RequiredParameter]
        public string AppSecret { get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            if (AppSecret == null)
            {
                Debug.WriteLine("[AppCenterTarget] AppSecret needs to be specified in NLog.config");
            }

            if (!AppCenter.Configured && AppSecret != null)
            {
                AppCenter.Start(AppSecret, typeof(Analytics), typeof(Crashes));
                Debug.WriteLine("[AppCenterTarget] AppCenter started");
            }

            var lo = this.Layout.ToString();
            string logMessage = this.Layout.Render(logEvent);
            Dictionary<string, string> props = new Dictionary<string, string>
            {
                {"LogLevel", logEvent.Level.ToString()},
                {"FullMessage", logMessage},

            };
            if (logEvent.Exception != null)
            {
                Crashes.TrackError(logEvent.Exception, props);
            }
            else
            {
                Analytics.TrackEvent(logEvent.LoggerName, props);
            }
        }


    }
}
