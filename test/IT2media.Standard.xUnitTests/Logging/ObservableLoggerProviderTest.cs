using System;
using System.Collections.Generic;
using System.Text;
using IT2media.Standard.Logging;
using Microsoft.Extensions.Logging;
using Xunit;

namespace IT2media.Standard.xUnitTests.Logging
{
    public class ObservableLoggerProviderTest
    {
        static LoggerFactory _loggerFactory = new LoggerFactory();
        [Fact]
        public void LogALot_Test()
        {
            _loggerFactory.AddObservableLogger();
            var logger = _loggerFactory.CreateLogger("xUnitTest");
            for (int i = 0; i < 10000; i++)
            {
                logger.LogDebug($"test {i}");
            }

        }
    }
}
