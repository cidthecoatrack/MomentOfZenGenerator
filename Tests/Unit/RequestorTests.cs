using MomentOfZenGenerator;
using MomentOfZenGenerator.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Tests.Unit
{
    [TestFixture]
    public class RequestorTests
    {
        private Mock<IYouTubeRequestUriBuilder> mockYoutubeRequestUriBuilder;
        private Mock<IResponseProvider> mockResponseProvider;
        private IRequestor requestor;

        [SetUp]
        public void Setup()
        {
            mockYoutubeRequestUriBuilder = new Mock<IYouTubeRequestUriBuilder>();
            mockResponseProvider = new Mock<IResponseProvider>();

            requestor = new Requestor(mockYoutubeRequestUriBuilder.Object, mockResponseProvider.Object);
        }

        [Test]
        public void RequestUriComesFromBuilder()
        {
            requestor.GetVideos(String.Empty);
            mockYoutubeRequestUriBuilder.Verify(b => b.BuildRequestUri(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void RequestUriIsBuiltWithSearchTerm()
        {
            requestor.GetVideos("search");
            mockYoutubeRequestUriBuilder.Verify(b => b.BuildRequestUri("search"), Times.Once);
        }

        [Test]
        public void ResponseComesFromResponseProvider()
        {
            requestor.GetVideos(String.Empty);
            mockResponseProvider.Verify(p => p.GetResponseContent(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void ResponseComesFromUriBuiltByYoutubeBuilder()
        {
            mockYoutubeRequestUriBuilder.Setup(b => b.BuildRequestUri(It.IsAny<String>())).Returns("uri");

            requestor.GetVideos(String.Empty);
            mockResponseProvider.Verify(p => p.GetResponseContent("uri"), Times.Once);
        }

        [Test]
        public void ReturnsContentFromResponseProvider()
        {
            mockResponseProvider.Setup(p => p.GetResponseContent(It.IsAny<String>())).Returns("content");

            var content = requestor.GetVideos(String.Empty);
            Assert.That(content, Is.EqualTo("content"));
        }
    }
}