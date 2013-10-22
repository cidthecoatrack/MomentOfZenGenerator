using MomentOfZenGenerator.Interfaces;
using System;
using System.Collections.Generic;

namespace MomentOfZenGenerator
{
    public class Filter : IFilter
    {
        private IYouTubeResponseProvider youTubeResponseProvider;

        public Filter(IYouTubeResponseProvider youTubeResponseProvider)
        {
            this.youTubeResponseProvider = youTubeResponseProvider;
        }

        public IEnumerable<String> GetVideoUrlsLessThanOneMinuteLong(String searchWord)
        {
            var videoProjections = youTubeResponseProvider.GetVideos(searchWord);
            var videoUrls = new List<String>();

            foreach (var videoProjection in videoProjections)
                if (videoProjection.Duration <= 60)
                    videoUrls.Add(videoProjection.Url);

            return videoUrls;
        }
    }
}