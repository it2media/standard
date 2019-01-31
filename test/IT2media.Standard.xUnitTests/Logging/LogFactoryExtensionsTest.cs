using System;
using System.IO;
using System.Xml;
using FluentAssertions;
using IT2media.Standard.Logging;
using Xunit;
using Microsoft.Extensions.Logging;

namespace IT2media.Standard.xUnitTests.Logging
{
    public class LogFactoryExtensionsTest 
    {
        static LoggerFactory _loggerFactory = new LoggerFactory();
        [Fact]
        public void AddObservableLogger_Call_NotFailed()
        {
            Action act = () =>
            {
               _loggerFactory.AddObservableLogger();
                var logger = _loggerFactory.CreateLogger("xUnitTest");
                logger.LogDebug("xunit test");
            };
            act.Should().NotThrow();
        }

        [Fact]
        public void AddObservableLogger_Enabled()
        {
            _loggerFactory.AddObservableLogger();
            var logger = _loggerFactory.CreateLogger("xUnitTest");
            var logCol = _loggerFactory.GetObservableLogHistory("xUnitTest");
            var initialCount = logCol.Count;
            logger.LogDebug("Test");
            logCol.Count.Should().Be(initialCount + 1);
        }
        [Fact]
        public void DisableObservableLogger_NotEnabled()
        {
            _loggerFactory.AddObservableLogger();
            var logger = _loggerFactory.CreateLogger("xUnitTest");
            var logCol = _loggerFactory.GetObservableLogHistory("xUnitTest");
            var initialCount = logCol.Count;
            _loggerFactory.DisableObservableLogger();
            logger.LogDebug("test");
            logCol.Count.Should().Be(initialCount);
        }

        [Fact]
        public void GetObservableLoggerKeys_AddDifferentKeys()
        {
            _loggerFactory.AddObservableLogger();
            var logger = _loggerFactory.CreateLogger("xUnitTest");
            logger.LogDebug("Test");
            var logger2 = _loggerFactory.CreateLogger("xUnitTest2");
            logger2.LogDebug("Test");
            var logger3 = _loggerFactory.CreateLogger("xUnitTest3");
            logger3.LogDebug("Test");
            var keys = _loggerFactory.GetObservableLoggerKeys();
            keys.Should().HaveCount(3);
            keys.Should().Contain("xUnitTest");
            keys.Should().Contain("xUnitTest2");
            keys.Should().Contain("xUnitTest3");
        }

        [Fact]
        public void GetObservableLogHistory_AddEntry()
        {
            _loggerFactory.AddObservableLogger();
            var logger = _loggerFactory.CreateLogger("xUnitTest");
            logger.LogDebug("This is a test");
            _loggerFactory.GetObservableLogHistory("xUnitTest");
        }

        [Fact]
        public void AddNLog()
        {
            if (File.Exists("test.log"))
            {
                File.Delete("test.log");
            }
            _loggerFactory.AddNLog("NLog.config");
            var logger = _loggerFactory.CreateLogger("xUnitTest");
            logger.LogDebug("This is a test");
            var text = File.ReadAllText("test.log");
            text.Should().Contain("This is a test");
        }

        [Fact]
        public void ReplaceNLogConfig()
        {
            if (File.Exists("test2.log"))
            {
                File.Delete("test2.log");
            }
            _loggerFactory.AddNLog("NLog.config");
            var logger = _loggerFactory.CreateLogger("xUnitTest");
            using (var stream = File.OpenRead("NLog2.config"))
            {
                _loggerFactory.ReplaceNLogConfig(XmlReader.Create(stream));
                logger.LogDebug("This is a test in another file");
                var text = File.ReadAllText("test2.log");
                text.Should().Contain("This is a test in another file");
            }
        }
    }
}
