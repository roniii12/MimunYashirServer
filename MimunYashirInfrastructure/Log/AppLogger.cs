using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MimunYashirInfrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirInfrastructure.Log
{
    public class AppGeneralLogger<T> : AppLogger<T>
    {
        public AppGeneralLogger(ILogger<T> _logger) : base(_logger, null)
        {

        }
    }
    public class AppLogger<T> : IAppLogger<T>
    {
        ILogger<T> logger;
        private readonly IAppLogContext context;
        public AppLogger(ILogger<T> _logger, IAppLogContext context)
        {
            this.logger = _logger;
            this.context = context;
        }

        public void Info(string message)
        {
            Info(message, null, AppModule.UNDEFINED, AppLayer.UNDEFINED);
        }
        public void Info(string message, object parameters, AppModule module, AppLayer layer)
        {
            var entry = createLogEntry(context);
            entry.Message = message;
            entry.Parameters = parameters;
            entry.Module = module;
            entry.Layer = layer;


            var logEvent = new AppLogEvent(entry.Message);
            logEvent = setContextValues(entry, logEvent);

            logger.Log(LogLevel.Information,
                    default(EventId), logEvent, (Exception)null, AppLogEvent.Formatter);
        }

        public void Warning(string message)
        {
            Warning(message, null);
        }

        public void Warning(string message, object parameters)
        {
            var entry = createLogEntry(context);
            entry.Message = message;
            entry.Parameters = parameters;

            var logEvent = new AppLogEvent(entry.Message);
            logEvent = setContextValues(entry, logEvent);

            logger.Log(LogLevel.Warning,
                    default(EventId), logEvent, null, AppLogEvent.Formatter);
        }

        public void Warning(ManagedException exception)
        {
            logException("", exception, LogLevel.Warning);
        }

        public void Error(string message)
        {
            Error(message, null);
        }

        public void Error(ManagedException exception)
        {
            Error("", exception);
        }

        public void Error(string message, Exception exception)
        {
            logException(message, exception, LogLevel.Error);
        }
        private void logException(string message, Exception exception, LogLevel logLevel)
        {
            var entry = createLogEntry(context);
            entry.Message = message;
            entry.Exception = exception;

            var logEvent = new AppLogEvent(entry.Message);
            logEvent = setContextValues(entry, logEvent);

            if (exception is not ManagedException) logger.Log(logLevel,
                   default(EventId), logEvent, exception ?? null, AppLogEvent.Formatter);
            else
            {
                var managedException = (ManagedException)exception;
                var allmessages = (entry.Message != null ? $"{entry.Message} -->" : "") + (managedException.Messages ?? "");
                logEvent = new AppLogEvent(allmessages);
                logEvent = setContextValues(entry, logEvent);
                logEvent.ReplaceProp("StackTrace", managedException.OriginalStackTrace ?? "");

                logger.Log(logLevel,
                default(EventId), logEvent, exception, AppLogEvent.Formatter);
            }
        }

        private AppLogEvent setContextValues(LogEntry entry, AppLogEvent logEvent, bool allFields = true)
        {
            if (allFields)
                logEvent.AddProp("Module", entry.Module)
                 .AddProp("Layer", entry.Layer)
                 .AddProp("Parameters", entry.Parameters ?? "")
                 .AddProp("StackTrace", "");

            return logEvent.AddProp("UserId", entry.UserId + "")
            .AddProp("LoggerAppVersion", entry.LoggerAppVersion ?? "")
            .AddProp("LoggerAppCompoment", entry.LoggerAppCompoment ?? "");
        }

        private LogEntry createLogEntry(IAppLogContext context)
        {
            var entry = new LogEntry()
            {
                UserId = context?.UserId ?? "",
                LoggerAppVersion = typeof(T).Assembly.GetName().Version.ToString(),
                LoggerAppCompoment = typeof(T).Assembly.GetName().Name
            };

            return entry;
        }
    }
}
