﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using FluentAssertions;
using IT2media.Standard.Logging;
using Xunit;
using FluentAssertions.Extensions;
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
            var logCol = LogFactoryExtensions.GetObservableLogHistory("xUnitTest");
            var initialCount = logCol.Count;
            logger.LogDebug("Test");
            logCol.Count.Should().Be(initialCount + 1);
        }
        [Fact]
        public void DisableObservableLogger_NotEnabled()
        {
            _loggerFactory.AddObservableLogger();
            var logger = _loggerFactory.CreateLogger("xUnitTest");
            var logCol = LogFactoryExtensions.GetObservableLogHistory("xUnitTest");
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
            var logger2 = _loggerFactory.CreateLogger("xUnitTest2");
            var logger3 = _loggerFactory.CreateLogger("xUnitTest3");
            var keys = LogFactoryExtensions.GetObservableLoggerKeys();
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
            LogFactoryExtensions.GetObservableLogHistory("xUnitTest");
        }

        [Fact]
        public void AddNLog()
        {
            if (System.IO.File.Exists("test.log"))
            {
                System.IO.File.Delete("test.log");
            }
            _loggerFactory.AddNLog("NLog.config");
            var logger = _loggerFactory.CreateLogger("xUnitTest");
            logger.LogDebug("This is a test");
            var text = System.IO.File.ReadAllText("test.log");
            text.Should().Contain("This is a test");
        }

        [Fact]
        public void ReplaceNLogConfig()
        {
            if (System.IO.File.Exists("test2.log"))
            {
                System.IO.File.Delete("test2.log");
            }
            _loggerFactory.AddNLog();
            var logger = _loggerFactory.CreateLogger("xUnitTest");
            using (var stream = System.IO.File.OpenRead("NLog2.config"))
            {
                _loggerFactory.ReplaceNLogConfig(XmlReader.Create(stream));
                logger.LogDebug("This is a test in another file");
                var text = System.IO.File.ReadAllText("test2.log");
                text.Should().Contain("This is a test in another file");
            }
        }
    }
}
