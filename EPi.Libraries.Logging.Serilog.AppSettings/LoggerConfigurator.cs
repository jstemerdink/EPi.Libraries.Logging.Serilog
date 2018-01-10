﻿// Copyright © 2018 Jeroen Stemerdink.
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
    using System;

    using EPiServer.ServiceLocation;

    using global::Serilog;
    using global::Serilog.Core;

    /// <summary>
    /// Class LoggerConfigurator.
    /// </summary>
    [ServiceConfiguration(ServiceType = typeof(ILoggerConfigurator), Lifecycle = ServiceInstanceScope.Singleton)]
    public class LoggerConfigurator : ILoggerConfigurator
    {
        /// <summary>
        /// The logger
        /// </summary>
        private Logger logger;

        /// <summary>
        /// Gets a <see cref="T:Serilog.ILogger" /> instance for the provided name.
        /// </summary>
        /// <param name="name">Name of the logger</param>
        /// <returns>A new <see cref="T:Serilog.ILogger" /> instance for the provided name.</returns>
        public ILogger GetLogger(string name)
        {
            return this.logger ?? (this.logger = new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger());
        }

        /// <summary>
        /// Gets a <see cref="T:Serilog.ILogger" /> instance.
        /// </summary>
        /// <returns>A new <see cref="T:Serilog.ILogger" /> instance.</returns>
        public ILogger GetLogger()
        {
            return this.GetLogger(null);
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            this.logger.Dispose();
        }
    }
}