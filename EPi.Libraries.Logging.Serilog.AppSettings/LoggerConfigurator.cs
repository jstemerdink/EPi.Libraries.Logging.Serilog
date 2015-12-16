using EPiServer.ServiceLocation;

using Serilog;

namespace EPi.Libraries.Logging.Serilog.AppSettings
{
    /// <summary>
    /// Class LoggerConfigurator.
    /// </summary>
    [ServiceConfiguration(ServiceType = typeof(ILoggerConfigurator), Lifecycle = ServiceInstanceScope.Singleton)]
    public class LoggerConfigurator : ILoggerConfigurator
    {
        private ILogger logger;

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <returns>Serilog.ILogger.</returns>
        public ILogger GetLogger()
        {
            return this.logger ?? (this.logger = new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger());
        }
    }
}
