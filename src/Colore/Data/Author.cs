// ---------------------------------------------------------------------------------------
// <copyright file="Author.cs" company="Corale">
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

    using Newtonsoft.Json;

    /// <summary>
    /// Contains author information for a Chroma application.
    /// </summary>
    public sealed class Author
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Author" /> class.
        /// </summary>
        /// <param name="name">Name of the author.</param>
        /// <param name="contact">Contact information for the author.</param>
        [JsonConstructor]
        public Author(string name, string contact)
        {
            if (name.Length > Constants.MaxAuthorNameLength)
            {
                throw new ArgumentException(
                    $"Author name is too long, max length is {Constants.MaxAuthorNameLength}",
                    nameof(name));
            }

            if (contact.Length > Constants.MaxAuthorContactLength)
            {
                throw new ArgumentException(
                    $"Author contact is too long, max length is {Constants.MaxAuthorContactLength}",
                    nameof(contact));
            }

            Name = name;
            Contact = contact;
        }

        /// <summary>
        /// Gets the name of the application author.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; }

        /// <summary>
        /// Gets contact information for the author.
        /// </summary>
        [JsonProperty("contact")]
        public string Contact { get; }
    }
}
