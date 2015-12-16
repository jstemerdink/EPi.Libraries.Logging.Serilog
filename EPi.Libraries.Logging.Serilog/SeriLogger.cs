using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EPiServer.Logging;
using EPiServer.ServiceLocation;

using Serilog;
using Serilog.Events;

namespace EPi.Libraries.Logging.Serilog
{
    /// <summary>
    /// Class SeriLogger.
    /// </summary>
    public class SeriLogger : EPiServer.Logging.ILogger
    {
        /// <summary>
        ///     The logger
        /// </summary>
        private readonly global::Serilog.ILogger logger;


        /// <summary>
        /// Gets or sets the logger configurator.
        /// </summary>
        /// <value>The logger configurator.</value>
        private Injected<ILoggerConfigurator> LoggerConfigurator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public SeriLogger()
        {
            this.logger = this.LoggerConfigurator.Service.GetLogger();
        }

        /// <summary>
        /// Determines whether logging at the specified level is enabled.
        /// </summary>
        /// <param name="level">The level to check.</param>
        /// <returns>
        /// <c>true</c> if logging on the provided level is enabled; otherwise <c>false</c>
        /// </returns>
        public bool IsEnabled(Level level)
        {
            try
            {
                return this.IsEnabled(MapLevel(level));
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        /// <summary>
        /// Logs the provided <paramref name="state"/> with the specified level.
        /// </summary>
        /// <typeparam name="TState">The type of the state object.</typeparam><typeparam name="TException">The type of the exception.</typeparam><param name="level">The criticality level of the log message.</param><param name="state">The state that should be logged.</param><param name="exception">The exception that should be logged.</param><param name="messageFormatter">The message formatter used to write the state to the log provider.</param><param name="boundaryType">The type at the boundary of the logging framework facing the code using the logging.</param>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        /// <exception cref="ArgumentOutOfRangeException">level</exception>
        public void Log<TState, TException>(Level level, TState state, TException exception, Func<TState, TException, string> messageFormatter, Type boundaryType) where TException : Exception
        {
            if (messageFormatter == null)
            {
                return;
            }

            LogEventLevel mappedLevel = MapLevel(level);

            if (!this.IsEnabled(mappedLevel))
            {
                return;
            }

            if (boundaryType != null && boundaryType != typeof(LoggerExtensions))
            {
                this.logger.ForContext(boundaryType);
            }

            this.logger.Write(mappedLevel, exception, messageFormatter(state, exception));
        }

        /// <summary>
        ///     Determines whether the specified level is enabled.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns><c>true</c> if the specified level is enabled; otherwise, <c>false</c>.</returns>
        public bool IsEnabled(LogEventLevel level)
        {
            return this.logger.IsEnabled(level);
        }

        /// <summary>
        ///     Maps the EPiServer level to the NLoge level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>LogLevel.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">level</exception>
        private static LogEventLevel MapLevel(Level level)
        {
            switch (level)
            {
                case Level.Trace:
                    return LogEventLevel.Verbose;
                case Level.Debug:
                    return LogEventLevel.Debug;
                case Level.Information:
                    return LogEventLevel.Information;
                case Level.Warning:
                    return LogEventLevel.Warning;
                case Level.Error:
                    return LogEventLevel.Error;
                case Level.Critical:
                    return LogEventLevel.Fatal;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level));
            }
        }
    }
}
