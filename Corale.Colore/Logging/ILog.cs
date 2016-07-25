// <copyright file="ILog.cs" company="Corale">
//     Copyright © 2015-2016 by Adam Hellberg and Brandon Scott.
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

    /// <summary>
    /// Describes a logger.
    /// </summary>
    internal interface ILog
    {
        /// <summary>
        /// Gets the name of the logger.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the configured level threshold.
        /// </summary>
        LogLevel Level { get; }

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="level">The level of the message.</param>
        /// <param name="message">The message to log.</param>
        void Log(LogLevel level, string message);

        /// <summary>
        /// Logs a message with an exception.
        /// </summary>
        /// <param name="level">The level of the message.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception object to include.</param>
        void Log(LogLevel level, string message, Exception exception);

        /// <summary>
        /// Logs a formatted message.
        /// </summary>
        /// <param name="level">The level of the message.</param>
        /// <param name="format">The format string.</param>
        /// <param name="args">Format parameters.</param>
        void LogFormat(LogLevel level, string format, params object[] args);

        /// <summary>
        /// Logs a message at the <see cref="LogLevel.Debug" /> level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void Debug(string message);

        /// <summary>
        /// Logs a message at the <see cref="LogLevel.Debug" /> level
        /// with an exception object.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception object to include.</param>
        void Debug(string message, Exception exception);

        /// <summary>
        /// Logs a formatted message at the <see cref="LogLevel.Debug" /> level.
        /// </summary>
        /// <param name="format">Format string.</param>
        /// <param name="args">Format parameters.</param>
        void DebugFormat(string format, params object[] args);

        /// <summary>
        /// Logs a message at the <see cref="LogLevel.Info" /> level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void Info(string message);

        /// <summary>
        /// Logs a message at the <see cref="LogLevel.Info" /> level
        /// with an exception object.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception object to include.</param>
        void Info(string message, Exception exception);

        /// <summary>
        /// Logs a formatted message at the <see cref="LogLevel.Info" /> level.
        /// </summary>
        /// <param name="format">Format string.</param>
        /// <param name="args">Format parameters.</param>
        void InfoFormat(string format, params object[] args);

        /// <summary>
        /// Logs a message at the <see cref="LogLevel.Warn" /> level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void Warn(string message);

        /// <summary>
        /// Logs a message at the <see cref="LogLevel.Warn" /> level
        /// with an exception object.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception object to include.</param>
        void Warn(string message, Exception exception);

        /// <summary>
        /// Logs a formatted message at the <see cref="LogLevel.Warn" /> level.
        /// </summary>
        /// <param name="format">Format string.</param>
        /// <param name="args">Format parameters.</param>
        void WarnFormat(string format, params object[] args);

        /// <summary>
        /// Logs a message at the <see cref="LogLevel.Error" /> level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void Error(string message);

        /// <summary>
        /// Logs a message at the <see cref="LogLevel.Error" /> level
        /// with an exception object.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception object to include.</param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Logs a formatted message at the <see cref="LogLevel.Error" /> level.
        /// </summary>
        /// <param name="format">Format string.</param>
        /// <param name="args">Format parameters.</param>
        void ErrorFormat(string format, params object[] args);

        /// <summary>
        /// Logs a message at the <see cref="LogLevel.Fatal" /> level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void Fatal(string message);

        /// <summary>
        /// Logs a message at the <see cref="LogLevel.Fatal" /> level
        /// with an exception object.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception object to include.</param>
        void Fatal(string message, Exception exception);

        /// <summary>
        /// Logs a formatted message at the <see cref="LogLevel.Fatal" /> level.
        /// </summary>
        /// <param name="format">Format string.</param>
        /// <param name="args">Format parameters.</param>
        void FatalFormat(string format, params object[] args);
    }
}
