using System;
using System.Collections.Generic;
using System.Text;
using IT2media.Standard.Logging;
using Microsoft.Extensions.Logging;
using Xunit;

namespace IT2media.Standard.xUnitTests.Logging.NLogTargets
{
    // testing is not supported, so we just test, if no exceptions come up
    public class AppCenterTargetTest
    {
        static LoggerFactory _loggerFactory = new LoggerFactory();
        [Fact]
        public void InitializeWithConfig()
        {
            _loggerFactory.AddNLog("NLogAppCenter.config");
            var logger = _loggerFactory.CreateLogger("xUnitTest");
        }
        [Fact]
        public void TestDebugLog()
        {
            _loggerFactory.AddNLog("NLogAppCenter.config");
            var logger = _loggerFactory.CreateLogger("xUnitTest");
            logger.LogDebug("This is a test");
        }
        [Fact]
        public void TestErrorWithException()
        {
            _loggerFactory.AddNLog("NLogAppCenter.config");
            var logger = _loggerFactory.CreateLogger("xUnitTest");
            logger.LogError("This is a test",new Exception("this is a test exception"));
        }

    }
}
