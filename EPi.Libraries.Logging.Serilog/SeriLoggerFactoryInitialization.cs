using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Logging;

namespace EPi.Libraries.Logging.Serilog
{
    /// <summary>
    /// Class SeriLoggerFactoryInitialization.
    /// </summary>
    [InitializableModule]
    public class SeriLoggerFactoryInitialization : IInitializableModule
    {
        /// <summary>
        ///     Check if the initialization has been done.
        /// </summary>
        private static bool initialized;

        #region Properties

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        /// <remarks>
        ///     Gets called as part of the EPiServer Framework initialization sequence. Note that it will be called
        ///     only once per AppDomain, unless the method throws an exception. If an exception is thrown, the initialization
        ///     method will be called repeatedly for each request reaching the site until the method succeeds.
        /// </remarks>
        /// <exception cref="ArgumentNullException">factory</exception>
        public void Initialize(InitializationEngine context)
        {
            // If there is no context, we can't do anything.
            if (context == null)
            {
                return;
            }

            // If already initialized, no need to do it again.
            if (initialized)
            {
                return;
            }

            LogManager.Instance.AddFactory(new SeriLoggerFactory());

            initialized = true;
        }

        /// <summary>
        ///     Resets the module into an uninitialized state.
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        /// <remarks>
        ///     <para>
        ///         This method is usually not called when running under a web application since the web app may be shut down very
        ///         abruptly, but your module should still implement it properly since it will make integration and unit testing
        ///         much simpler.
        ///     </para>
        ///     <para>
        ///         Any work done by
        ///         <see
        ///             cref="M:EPiServer.Framework.IInitializableModule.Initialize(EPiServer.Framework.Initialization.InitializationEngine)" />
        ///         as well as any code executing on
        ///         <see cref="E:EPiServer.Framework.Initialization.InitializationEngine.InitComplete" />
        ///         should be reversed.
        ///     </para>
        /// </remarks>
        public void Uninitialize(InitializationEngine context)
        {
        }

        #endregion
    }
}
