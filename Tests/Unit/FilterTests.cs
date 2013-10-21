using System;
using MomentOfZenGenerator;
using MomentOfZenGenerator.Interfaces;
using Moq;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class FilterTests
    {
        private Filter filter;
        private Mock<IRequestor> mockRequestor;

        private const String xml = "";

        [SetUp]
        public void Setup()
        {
            mockRequestor = new Mock<IRequestor>();
            mockRequestor.Setup(r => r.GetVideos(It.IsAny<String>())).Returns(xml);

            filter = new Filter(mockRequestor.Object);
        }

        [Test]
        public void FilterGetsVideosFromRequestor()
        {
            filter.GetVideoUrlsLessThanOneMinuteLong(String.Empty);
            mockRequestor.Verify(r => r.GetVideos(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void FilterGetsVideosFromRequestorWithSearchTerm()
        {
            filter.GetVideoUrlsLessThanOneMinuteLong("search");
            mockRequestor.Verify(r => r.GetVideos("search"), Times.Once);
        }
    }
}