using System;
using System.Linq;
using MomentOfZenGenerator.Interfaces;

namespace MomentOfZenGenerator
{
    public class Generator
    {
        private Random random;
        private IWordnikResponseProvider wordnikResponseProvider;
        private IFilter filter;

        public Generator(Random random, IWordnikResponseProvider wordnikResponseProvider, IFilter filter)
        {
            this.random = random;
            this.wordnikResponseProvider = wordnikResponseProvider;
            this.filter = filter;
        }

        public String GetMomentOfZen()
        {
            var videoUrls = Enumerable.Empty<String>();

            var word = wordnikResponseProvider.GetWord();
            videoUrls = filter.GetVideoUrlsLessThanOneMinuteLong(word);

            if (!videoUrls.Any())
                return String.Empty;

            var video = videoUrls.ElementAt(random.Next(videoUrls.Count()));
            return video;
        }
    }
}