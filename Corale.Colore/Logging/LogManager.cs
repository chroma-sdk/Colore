// <copyright file="LogManager.cs" company="Corale">
//     Copyright © 2015 by Adam Hellberg and Brandon Scott.
//
//     Permission is hereby granted, free of charge, to any person obtaining a copy of
//     this software and associated documentation files (the "Software"), to deal in
//     the Software without restriction, including without limitation the rights to
//     use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//     of the Software, and to permit persons to whom the Software is furnished to do
//     so, subject to the following conditions:
//
//     The above copyright notice and this permission notice shall be included in all
//     copies or substantial portions of the Software.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//     CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>

namespace Corale.Colore.Logging
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides methods to get loggers for classes.
    /// </summary>
    internal static class LogManager
    {
        /// <summary>
        /// Caches created logger objects by name.
        /// </summary>
        private static readonly Dictionary<string, ILog> Cache = new Dictionary<string, ILog>();

        /// <summary>
        /// Gets a logger instance with the specified name.
        /// </summary>
        /// <param name="name">The name to give the logger.</param>
        /// <param name="level">The default level to set for the logger.</param>
        /// <returns>An instance of a logger with the specified name.</returns>
        internal static ILog GetLogger(string name, LogLevel level = LogLevel.Debug)
        {
            if (Cache.ContainsKey(name))
                return Cache[name];

            var logger = new TraceLogger(name, level);

            Cache[name] = logger;

            return logger;
        }

        /// <summary>
        /// Gets a logger instance for the specified <see cref="Type" />.
        /// </summary>
        /// <param name="type">The type to get a logger instance for.</param>
        /// <param name="level">The default level to set for the logger.</param>
        /// <returns>An instance of a logger implementing <see cref="ILog" />.</returns>
        internal static ILog GetLogger(Type type, LogLevel level = LogLevel.Debug)
        {
            return GetLogger(type.FullName, level);
        }
    }
}
