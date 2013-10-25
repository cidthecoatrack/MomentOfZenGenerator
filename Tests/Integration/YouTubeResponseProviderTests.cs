using System.Linq;
using MomentOfZenGenerator.Interfaces;
using MomentOfZenGenerator.YouTube;
using NUnit.Framework;

namespace Tests.Integration
{
    public class YouTubeResponseProviderTests
    {
        private IYouTubeResponseProvider provider;

        [SetUp]
        public void Setup()
        {
            provider = new YouTubeResponseProvider();
        }

        [Test]
        public void ResponseContainsEntries()
        {
            var videos = provider.GetVideos("video");
            Assert.That(videos.Any(), Is.True);
        }
    }
}