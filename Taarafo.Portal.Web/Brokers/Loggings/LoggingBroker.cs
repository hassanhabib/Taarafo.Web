// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Microsoft.Extensions.Logging;

namespace Taarafo.Portal.Web.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger logger;

        public LoggingBroker(ILogger logger) => this.logger = logger;

        public void LogDebug(string message) => this.logger.LogDebug(message);
        public void LogInformation(string message) => this.logger.LogInformation(message);
        public void LogTrace(string message) => this.logger.LogTrace(message);
        public void LogWarning(string message) => this.logger.LogWarning(message);

        public void LogError(Exception exception) =>
            this.logger.LogError(exception, exception.Message);

        public void LogCritical(Exception exception) =>
            this.logger.LogCritical(exception, exception.Message);
    }
}
