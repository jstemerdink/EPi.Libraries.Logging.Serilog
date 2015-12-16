NOTE: You will need a Logger configuration. You can either use EPi.Libraries.Logging.Serilog.AppSettings, so you can configure Serilog in the AppSettings 
or you can create your own implementation.

    [ServiceConfiguration(ServiceType = typeof(ILoggerConfigurator), Lifecycle = ServiceInstanceScope.Singleton)]
    public class LoggerConfigurator : ILoggerConfigurator
    {
        public ILogger GetLogger()
        {
            return your own configuration here;
        }
    }

