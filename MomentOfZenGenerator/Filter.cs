using System;
using System.Collections.Generic;
using System.Linq;
using MomentOfZenGenerator.Interfaces;
using MomentOfZenGenerator.YouTube;

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
            var filteredVideos = videoProjections.Where(v => v.Duration <= 60);
            return filteredVideos.Select<YouTubeVideoProjection, String>(v => v.Url);
        }
    }
}