// Copyright © 2018 Jeroen Stemerdink.
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
    using System.Web;

    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using EPiServer.ServiceLocation;

    /// <summary>
    /// Class SeriLoggerInitializationModule.
    /// </summary>
    /// <seealso cref="EPiServer.Framework.IInitializableHttpModule" />
    [InitializableModule]
    [ModuleDependency(typeof(InitializationModule))]
    public class SeriLoggerInitializationModule : IInitializableHttpModule
    {
        private bool loggerClosedAndFlushed;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <remarks>Gets called as part of the EPiServer Framework initialization sequence. Note that it will be called
        /// only once per AppDomain, unless the method throws an exception. If an exception is thrown, the initialization
        /// method will be called repeadetly for each request reaching the site until the method succeeds.</remarks>
        public void Initialize(InitializationEngine context)
        {
        }

        /// <summary>
        ///     Initialize any events on <see c="System.Web.HttpApplication" /> instances created by ASP.NET
        /// </summary>
        /// <param name="application">The instance to initialize</param>
        /// <remarks>
        ///     <para>
        ///         This method may be called either after or before the Initialize-method so make sure you do not have any
        ///         dependencies between these methods. The beaviour that is guaranteed
        ///         is that your event handlers will not be called before the Initialize-method. If EPiServer is being initialized
        ///         outside ASP.NET this method will never get called.
        ///     </para>
        ///     <para>
        ///         When the ASP.NET runtime initializes it will create many instances of the
        ///         <see c="System.Web.HttpApplication" /> class so make sure you only add
        ///         logic to initialize events in this method, and not any one-time configuration which should be placed in the
        ///         Initialize-method.
        ///     </para>
        ///     <para>
        ///         Since a <see c="System.Web.HttpApplication" /> can be disposed at any time make sure you never store
        ///         references to this class since that could create a memory leak.
        ///     </para>
        ///     <para>Don't add initialization logic in this method.</para>
        /// </remarks>
        public void InitializeHttpEvents(HttpApplication application)
        {
            if (application == null)
            {
                return;
            }

            application.Disposed += this.ApplicationDisposed;
        }

        /// <summary>
        /// Resets the module into an uninitialized state.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <remarks><para>
        /// This method is usually not called when running under a web application since the web app may be shut down very
        /// abruptly, but your module should still implement it properly since it will make integration and unit testing
        /// much simpler.
        /// </para>
        /// <para>
        /// Any work done by <see cref="M:EPiServer.Framework.IInitializableModule.Initialize(EPiServer.Framework.Initialization.InitializationEngine)" /> as well as any code executing on <see cref="E:EPiServer.Framework.Initialization.InitializationEngine.InitComplete" /> should be reversed.
        /// </para></remarks>
        public void Uninitialize(InitializationEngine context)
        {
            this.CloseAndFlushLogger();
        }

        /// <summary>
        ///     Application is disposed
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ApplicationDisposed(object sender, EventArgs e)
        {
            this.CloseAndFlushLogger();
        }

        private void CloseAndFlushLogger()
        {
            if (this.loggerClosedAndFlushed)
            {
                return;
            }

            // Close and flush logger when app pool recycles or app is restarted.
            ILoggerConfigurator configurator;

            if (!ServiceLocator.Current.TryGetExistingInstance(instance: out configurator))
            {
                return;
            }

            configurator.Dispose();
            this.loggerClosedAndFlushed = true;
        }
    }
}
