// <copyright file="TraceLogger.cs" company="Corale">
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
    using System.Diagnostics;

    /// <summary>
    /// A logger using <c>System.Diagnostics</c> to log messages.
    /// </summary>
    internal sealed class TraceLogger : Logger
    {
        /// <summary>
        /// Maps internal logger levels to the ones used in <c>System.Diagnostics</c>.
        /// </summary>
        private static readonly Dictionary<LogLevel, SourceLevels> LevelMapping = new Dictionary<LogLevel, SourceLevels>
        {
            [LogLevel.Debug] = SourceLevels.Verbose,
            [LogLevel.Info] = SourceLevels.Information,
            [LogLevel.Warn] = SourceLevels.Warning,
            [LogLevel.Error] = SourceLevels.Error,
            [LogLevel.Fatal] = SourceLevels.Critical
        };

        /// <summary>
        /// Maps internal logger levels to event types for use with <c>System.Diagnostics</c>.
        /// </summary>
        private static readonly Dictionary<LogLevel, TraceEventType> EventMapping = new Dictionary
            <LogLevel, TraceEventType>
        {
            [LogLevel.Debug] = TraceEventType.Verbose,
            [LogLevel.Info] = TraceEventType.Information,
            [LogLevel.Warn] = TraceEventType.Warning,
            [LogLevel.Error] = TraceEventType.Error,
            [LogLevel.Fatal] = TraceEventType.Critical
        };

        /// <summary>
        /// Caches created <see cref="TraceSource" /> objects.
        /// </summary>
        private static readonly Dictionary<string, TraceSource> SourceCache = new Dictionary<string, TraceSource>();

        /// <summary>
        /// The internal <see cref="TraceSource" /> object that this logger uses.
        /// </summary>
        private readonly TraceSource _source;

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceLogger" /> class.
        /// </summary>
        /// <param name="name">The name of this logger.</param>
        /// <param name="level">The level to log at.</param>
        internal TraceLogger(string name, LogLevel level = LogLevel.Info)
            : base(name, level)
        {
            lock (SourceCache)
            {
                if (SourceCache.TryGetValue(Name, out _source))
                    return;

                _source = new TraceSource(Name, LevelMapping[Level]);

                Configure();
            }
        }

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="level">The level of the message.</param>
        /// <param name="message">The message to log.</param>
        public override void Log(LogLevel level, string message)
        {
            _source.TraceEvent(EventMapping[level], 0, message);
        }

        /// <summary>
        /// Logs a message with an exception.
        /// </summary>
        /// <param name="level">The level of the message.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception object to include.</param>
        public override void Log(LogLevel level, string message, Exception exception)
        {
            _source.TraceData(EventMapping[level], 0, message, exception);
        }

        /// <summary>
        /// Logs a formatted message.
        /// </summary>
        /// <param name="level">The level of the message.</param>
        /// <param name="format">The format string.</param>
        /// <param name="args">Format parameters.</param>
        public override void LogFormat(LogLevel level, string format, params object[] args)
        {
            _source.TraceEvent(EventMapping[level], 0, format, args);
        }

        /// <summary>
        /// Returns whether the <see cref="TraceSource" /> object has
        /// a non-default configuration applied.
        /// </summary>
        /// <param name="source">An instance of <see cref="TraceSource" /> to check.</param>
        /// <returns><c>true</c> if the source has been configured explicitly, otherwise <c>false</c>.</returns>
        private static bool IsConfigured(TraceSource source)
        {
            return source.Listeners.Count != 1 || !(source.Listeners[0] is DefaultTraceListener) ||
                   source.Listeners[0].Name != "Default";
        }

        /// <summary>
        /// Gets the parent <see cref="TraceSource" /> object of a named
        /// trace source.
        /// </summary>
        /// <param name="name">The name of the child trace source.</param>
        /// <param name="level">The trace level.</param>
        /// <returns>
        /// An instance of <see cref="TraceSource" /> that is the logical
        /// parent by name to the specified trace source.
        /// </returns>
        private static TraceSource GetParent(string name, SourceLevels level)
        {
            if (string.IsNullOrEmpty(name))
                return new TraceSource("Default", level);

            var source = new TraceSource(name, level);

            if (IsConfigured(source))
                return source;

            var dotIndex = name.LastIndexOf(".", StringComparison.Ordinal);

            // ReSharper disable once TailRecursiveCall
            return dotIndex == -1 ? GetParent(null, level) : GetParent(name.Substring(0, dotIndex), level);
        }

        /// <summary>
        /// Configures this logger instance.
        /// </summary>
        private void Configure()
        {
            if (IsConfigured(_source))
            {
                SourceCache[Name] = _source;
                return;
            }

            var parent = GetParent(Name, LevelMapping[Level]);

            _source.Switch = parent.Switch;

            _source.Listeners.Clear();

            foreach (TraceListener listener in parent.Listeners)
            {
                _source.Listeners.Add(listener);
            }

            SourceCache[Name] = _source;
        }
    }
}
