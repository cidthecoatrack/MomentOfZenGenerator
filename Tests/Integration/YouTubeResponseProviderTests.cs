using System;
using System.Collections.Generic;
using System.Linq;
using MomentOfZenGenerator.YouTube;
using NUnit.Framework;

namespace Tests.Integration
{
    public class YouTubeResponseProviderTests
    {
        private IEnumerable<YouTubeVideoProjection> videos;
        private const String baseYoutubeEmbedUrl = "https://www.youtube.com/embed/";

        [SetUp]
        public void Setup()
        {
            var provider = new YouTubeResponseProvider();
            videos = provider.GetVideos("video");
        }

        [Test]
        public void ResponseContainsEntries()
        {
            Assert.That(videos.Any(), Is.True);
        }

        [Test]
        public void ResponseHasUrlsToEmbeddedYoutubeVideo()
        {
            Assert.That(videos.All(v => v.Url.StartsWith(baseYoutubeEmbedUrl)), Is.True);
        }

        [Test]
        public void ResponseEndsWith11CharacterVideoId()
        {
            foreach (var video in videos)
            {
                var ending = video.Url.Replace(baseYoutubeEmbedUrl, String.Empty);
                Assert.That(ending.Length, Is.EqualTo(11));
            }
        }
    }
}