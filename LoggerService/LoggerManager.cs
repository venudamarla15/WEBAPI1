﻿using Contracts;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = (ILogger)LogManager.GetCurrentClassLogger();
        public LoggerManager()
        {
        }
        public void LogDebug(string message) => logger.LogDebug(message);
        public void LogError(string message) => logger.LogError(message);
        public void LogInfo(string message) => logger.LogInformation(message);
        public void LogWarn(string message) => logger.LogWarning(message);

    }
}
