// Copyright © 2016 Jeroen Stemerdink.
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
namespace EPi.Libraries.Logging.Serilog
{
    using System;

    using EPiServer.Logging;
    using EPiServer.ServiceLocation;

    using global::Serilog.Events;

    using ILogger = global::Serilog.ILogger;

    /// <summary>
    /// Class SeriLogger. 
    /// </summary>
    public class SeriLogger : EPiServer.Logging.ILogger
    {
        /// <summary>
        ///     The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public SeriLogger()
        {
            if (this.logger == null)
            {
                this.logger = this.LoggerConfigurator.Service.GetLogger();
            }
        }

        /// <summary>
        /// Gets or sets the logger configurator.
        /// </summary>
        /// <value>The logger configurator.</value>
        private Injected<ILoggerConfigurator> LoggerConfigurator { get; set; }

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
        ///     Determines whether the specified level is enabled.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns><c>true</c> if the specified level is enabled; otherwise, <c>false</c>.</returns>
        public bool IsEnabled(LogEventLevel level)
        {
            return this.logger.IsEnabled(level);

            // return global::Serilog.Log.IsEnabled(level);
        }

        /// <summary>
        /// Logs the provided <paramref name="state"/> with the specified level.
        /// </summary>
        /// <typeparam name="TState">The type of the state object.</typeparam><typeparam name="TException">The type of the exception.</typeparam><param name="level">The criticality level of the log message.</param><param name="state">The state that should be logged.</param><param name="exception">The exception that should be logged.</param><param name="messageFormatter">The message formatter used to write the state to the log provider.</param><param name="boundaryType">The type at the boundary of the logging framework facing the code using the logging.</param>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        /// <exception cref="ArgumentOutOfRangeException">level</exception>
        public void Log<TState, TException>(
            Level level, 
            TState state, 
            TException exception, 
            Func<TState, TException, string> messageFormatter, 
            Type boundaryType) where TException : Exception
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

            if ((boundaryType != null) && (boundaryType != typeof(LoggerExtensions)))
            {
                this.logger.ForContext(boundaryType);

                // global::Serilog.Log.ForContext(boundaryType);
            }

            this.logger.Write(mappedLevel, exception, messageFormatter(state, exception));

            // global::Serilog.Log.Write(mappedLevel, exception, messageFormatter(state, exception)); 
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
                    throw new ArgumentOutOfRangeException("level");
            }
        }
    }
}