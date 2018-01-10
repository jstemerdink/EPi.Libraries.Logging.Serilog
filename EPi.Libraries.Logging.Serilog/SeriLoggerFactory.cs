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
    using EPiServer.Logging;

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
            return new SeriLogger(name);
        }
    }
}