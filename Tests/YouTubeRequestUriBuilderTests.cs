using System;
using MomentOfZenGenerator;
using MomentOfZenGenerator.Interfaces;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class YouTubeRequestUriBuilderTests
    {
        private IRequestUriBuilder builder;

        [SetUp]
        public void Setup()
        {
            builder = new YouTubeRequestUriBuilder();
        }

        [Test]
        public void RequestUriHasYoutubeApiAsBase()
        {
            var uri = builder.BuildRequestUrl(String.Empty);
            Assert.That(uri.StartsWith("https://gdata.youtube.com/feeds/api/videos"), Is.True);
        }

        [Test]
        public void RequestUriHasQueryFilter()
        {
            var uri = builder.BuildRequestUrl("search");
            Assert.That(uri.Contains("q=search"), Is.True);
        }

        [Test]
        public void RequestUriHasFieldFilters()
        {
            var uri = builder.BuildRequestUrl(String.Empty);
            Assert.That(uri.Contains("fields=entry(media:group(media:player,yt:duration))"), Is.True);
        }

        [Test]
        public void RequestUriHasDurationFilters()
        {
            var uri = builder.BuildRequestUrl(String.Empty);
            Assert.That(uri.Contains("duration=short"), Is.True);
        }

        [Test]
        public void RequestUriUsesYoutubeApiVersionTwo()
        {
            var uri = builder.BuildRequestUrl(String.Empty);
            Assert.That(uri.Contains("v=2"), Is.True);
        }
    }
}