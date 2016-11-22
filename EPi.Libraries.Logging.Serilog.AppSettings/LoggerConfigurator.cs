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
namespace EPi.Libraries.Logging.Serilog.AppSettings
{
    using EPiServer.ServiceLocation;

    using global::Serilog;

    /// <summary>
    /// Class LoggerConfigurator.
    /// </summary>
    [ServiceConfiguration(ServiceType = typeof(ILoggerConfigurator), Lifecycle = ServiceInstanceScope.Singleton)]
    public class LoggerConfigurator : ILoggerConfigurator
    {
        private ILogger logger;

        /// <summary>
        /// Gets the logger with the specified name
        /// </summary>
        /// <param name="name">Name of the logger</param>
        /// <returns>Serilog.ILogger with the passed in name.</returns>
        public ILogger GetLogger(string name)
        {
            return this.logger ?? (this.logger = new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger());
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <returns>Serilog.ILogger.</returns>
        public ILogger GetLogger()
        {
            return GetLogger(null);
        }
    }
}