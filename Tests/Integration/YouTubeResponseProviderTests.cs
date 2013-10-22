using MomentOfZenGenerator.Interfaces;
using MomentOfZenGenerator.YouTube;
using NUnit.Framework;
using System.Linq;

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
            var videos = provider.GetVideos("pentatonix");
            Assert.That(videos.Any(), Is.True);
        }
    }
}