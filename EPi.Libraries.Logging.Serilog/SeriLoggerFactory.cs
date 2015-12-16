using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EPiServer.Logging;

namespace EPi.Libraries.Logging.Serilog
{
    /// <summary>
    /// Class SeriLoggerFactory.
    /// </summary>
    public class SeriLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Creates a <see cref="T:EPiServer.Logging.ILogger"/> with the provided name.
        /// </summary>
        /// <param name="name">The name of the logger to create.</param>
        /// <returns>
        /// An <see cref="T:EPiServer.Logging.ILogger"/> instance with the provided name.
        /// </returns>
        public ILogger Create(string name)
        {
            return new SeriLogger();
        }
    }
}
