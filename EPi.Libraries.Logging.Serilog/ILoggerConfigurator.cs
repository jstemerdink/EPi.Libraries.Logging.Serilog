using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPi.Libraries.Logging.Serilog
{
    /// <summary>
    /// Interface ILoggerConfigurator
    /// </summary>
    public interface ILoggerConfigurator
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <returns>Serilog.ILogger.</returns>
        global::Serilog.ILogger GetLogger();
    }
}
