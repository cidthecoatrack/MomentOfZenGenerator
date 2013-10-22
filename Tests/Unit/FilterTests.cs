using MomentOfZenGenerator;
using MomentOfZenGenerator.Interfaces;
using MomentOfZenGenerator.YouTube;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Unit
{
    [TestFixture]
    public class FilterTests
    {
        private Filter filter;
        private Mock<IYouTubeResponseProvider> mockProvider;

        private List<YouTubeVideoProjection> videos;
        private YouTubeVideoProjection videoProjection;

        [SetUp]
        public void Setup()
        {
            videoProjection = new YouTubeVideoProjection();

            videos = new List<YouTubeVideoProjection>();
            videos.Add(videoProjection);

            mockProvider = new Mock<IYouTubeResponseProvider>();
            mockProvider.Setup(p => p.GetVideos(It.IsAny<String>())).Returns(videos);

            filter = new Filter(mockProvider.Object);
        }

        [Test]
        public void FilterGetsVideosFromProvider()
        {
            filter.GetVideoUrlsLessThanOneMinuteLong(String.Empty);
            mockProvider.Verify(p => p.GetVideos(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void FilterGetsVideosFromProviderWithSearchTerm()
        {
            filter.GetVideoUrlsLessThanOneMinuteLong("search");
            mockProvider.Verify(p => p.GetVideos("search"), Times.Once);
        }

        [Test]
        public void FilterGetsMultipleVideosFromProvider()
        {
            for (var i = 0; i < 10; i++)
                videos.Add(new YouTubeVideoProjection());

            var filteredVideos = filter.GetVideoUrlsLessThanOneMinuteLong(String.Empty);
            Assert.That(filteredVideos.Count(), Is.EqualTo(videos.Count));
        }

        [Test]
        public void GetsVideosUpTo60SecondsLong()
        {
            for (var duration = 1; duration <= 60; duration++)
            {
                videoProjection.Duration = duration;
                videoProjection.Url = duration.ToString();

                var filteredVideoUrls = filter.GetVideoUrlsLessThanOneMinuteLong(String.Empty);
                Assert.That(filteredVideoUrls.First(), Is.EqualTo(duration.ToString()));
            }
        }

        [Test]
        public void RejectsVideosLongerThan60Seconds()
        {
            var otherVideoProjection = new YouTubeVideoProjection();
            otherVideoProjection.Duration = 60;
            otherVideoProjection.Url = "video that is just right";
            videos.Add(otherVideoProjection);

            videoProjection.Duration = 61;
            videoProjection.Url = "video that is too long";

            var filteredVideoUrls = filter.GetVideoUrlsLessThanOneMinuteLong(String.Empty);
            Assert.That(filteredVideoUrls.Count(), Is.EqualTo(1));
            Assert.That(filteredVideoUrls.First(), Is.EqualTo("video that is just right"));
        }
    }
}