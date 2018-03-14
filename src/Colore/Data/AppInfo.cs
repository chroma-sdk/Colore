// ---------------------------------------------------------------------------------------
// <copyright file="AppInfo.cs" company="Corale">
//     Copyright © 2015-2017 by Adam Hellberg and Brandon Scott.
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
// ---------------------------------------------------------------------------------------

namespace Colore.Data
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about an application wishing to use the Chroma SDK.
    /// </summary>
    public sealed class AppInfo
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="AppInfo" /> class, set to support all available devices.
        /// </summary>
        /// <param name="title">Application title.</param>
        /// <param name="description">Application description.</param>
        /// <param name="authorName">Name of the application author.</param>
        /// <param name="authorContact">Contact information for the author.</param>
        /// <param name="category">Application category.</param>
        public AppInfo(string title, string description, string authorName, string authorContact, Category category)
            : this(title, description, authorName, authorContact, null, category)
        {
            SupportedDevices = new[]
            {
                DeviceType.Keyboard,
                DeviceType.Mouse,
                DeviceType.Headset,
                DeviceType.Keypad,
                DeviceType.Mousepad,
                DeviceType.ChromaLink
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInfo" /> class.
        /// </summary>
        /// <param name="title">Application title.</param>
        /// <param name="description">Application description.</param>
        /// <param name="authorName">Name of the application author.</param>
        /// <param name="authorContact">Contact information for the author.</param>
        /// <param name="supportedDevices">List of devices this application supports.</param>
        /// <param name="category">Application category.</param>
        public AppInfo(
            string title,
            string description,
            string authorName,
            string authorContact,
            IEnumerable<DeviceType> supportedDevices,
            Category category)
        {
            if (title.Length > Constants.MaxAppTitleLength)
            {
                throw new ArgumentException(
                    $"Application name too long, max length is {Constants.MaxAppTitleLength}",
                    nameof(title));
            }

            if (description.Length > Constants.MaxAppDescriptionLength)
            {
                throw new ArgumentException(
                    $"Application description is too long, max length is {Constants.MaxAppDescriptionLength}",
                    nameof(description));
            }

            Title = title;
            Description = description;
            Author = new Author(authorName, authorContact);
            SupportedDevices = supportedDevices;
            Category = category;
        }

        /// <summary>
        /// Gets the title of this application.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; }

        /// <summary>
        /// Gets the application description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; }

        /// <summary>
        /// Gets author information for this application.
        /// </summary>
        [JsonProperty("author")]
        public Author Author { get; }

        /// <summary>
        /// Gets a list of devices this application supports.
        /// </summary>
        /// <remarks>
        /// Newtonsoft.Json cannot deserialize into an <see cref="IEnumerable{T}" />,
        /// but since we only serialize this class, it will not be an issue.
        /// </remarks>
        [JsonProperty("device_supported")]
        public IEnumerable<DeviceType> SupportedDevices { get; }

        /// <summary>
        /// Gets the category of this application.
        /// </summary>
        [JsonProperty("category")]
        public Category Category { get; }
    }
}
