// ---------------------------------------------------------------------------------------
// <copyright file="RestClientTests.cs" company="Corale">
//     Copyright © 2015-2019 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Tests.Rest
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Colore.Rest;
    using Colore.Tests.Mocking;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class RestClientTests
    {
        [Test]
        public void ShouldConstructWithCorrectBaseAddressFromUri()
        {
            var uri = new Uri("http://google.com");
            var client = new RestClient(uri);
            Assert.AreEqual(uri, client.BaseAddress);
            client.Dispose();
        }

        [Test]
        public void ShouldConstructWithCorrectBaseAddressFromString()
        {
            const string Address = "https://example.org/mysite";
            var client = new RestClient(Address);
            Assert.AreEqual(new Uri(Address), client.BaseAddress);
            client.Dispose();
        }

        [Test]
        public void ShouldChangeBaseAddress()
        {
            var oldUri = new Uri("http://example.com");
            var newUri = new Uri("http://example.org");
            var client = new RestClient(oldUri) { BaseAddress = newUri };
            Assert.AreEqual(newUri, client.BaseAddress);
            client.Dispose();
        }

        [Test]
        public async Task ShouldPerformSimplePost()
        {
            var (mockMessageHandler, client) = CreateMockClient();

            mockMessageHandler.Setup(
                                  h => h.SendAsync(
                                      It.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Post),
                                      It.IsAny<CancellationToken>()))
                              .ReturnsAsync(
                                  new HttpResponseMessage(HttpStatusCode.OK)
                                  {
                                      Content = new StringContent("\"hello\"")
                                  });

            await client.PostAsync<string>("test", null);

            mockMessageHandler.Verify(
                h => h.SendAsync(
                    It.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Post),
                    It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task ShouldPerformSimplePut()
        {
            var (mockMessageHandler, client) = CreateMockClient();

            mockMessageHandler.Setup(
                                  h => h.SendAsync(
                                      It.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Put),
                                      It.IsAny<CancellationToken>()))
                              .ReturnsAsync(
                                  new HttpResponseMessage(HttpStatusCode.OK)
                                  {
                                      Content = new StringContent("\"hello\"")
                                  });

            await client.PutAsync<string>("test");

            mockMessageHandler.Verify(
                h => h.SendAsync(
                    It.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Put),
                    It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task ShouldPerformPutWithData()
        {
            var (mockMessageHandler, client) = CreateMockClient();

            mockMessageHandler.Setup(
                                  h => h.SendAsync(
                                      It.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Put),
                                      It.IsAny<CancellationToken>()))
                              .ReturnsAsync(
                                  new HttpResponseMessage(HttpStatusCode.OK)
                                  {
                                      Content = new StringContent("\"hello\"")
                                  });

            await client.PutAsync<string>("test", "foobar");

            mockMessageHandler.Verify(
                h => h.SendAsync(
                    It.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Put && r.Content is StringContent),
                    It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task ShouldPerformSimpleDelete()
        {
            var (mockMessageHandler, client) = CreateMockClient();

            mockMessageHandler.Setup(
                                  h => h.SendAsync(
                                      It.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Delete),
                                      It.IsAny<CancellationToken>()))
                              .ReturnsAsync(
                                  new HttpResponseMessage(HttpStatusCode.OK)
                                  {
                                      Content = new StringContent("\"hello\"")
                                  });

            await client.DeleteAsync<string>("test");

            mockMessageHandler.Verify(
                h => h.SendAsync(
                    It.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Delete),
                    It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task ShouldPerformDeleteWithData()
        {
            var (mockMessageHandler, client) = CreateMockClient();

            mockMessageHandler.Setup(
                                  h => h.SendAsync(
                                      It.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Delete),
                                      It.IsAny<CancellationToken>()))
                              .ReturnsAsync(
                                  new HttpResponseMessage(HttpStatusCode.OK)
                                  {
                                      Content = new StringContent("\"hello\"")
                                  });

            await client.DeleteAsync<string>("test", "foobar");

            mockMessageHandler.Verify(
                h => h.SendAsync(
                    It.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Delete && r.Content is StringContent),
                    It.IsAny<CancellationToken>()));
        }

        private static (Mock<IMockHttpMessageHandler>, RestClient) CreateMockClient()
        {
            var mockMessageHandler = new Mock<IMockHttpMessageHandler>();
            var client = new RestClient(
                new Uri("http://example.org"),
                new MockHttpMessageHandler(mockMessageHandler.Object));
            return (mockMessageHandler, client);
        }
    }
}
