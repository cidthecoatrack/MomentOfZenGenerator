using System;
using System.Collections.Generic;
using System.Linq;
using MomentOfZenGenerator.Interfaces;
using MomentOfZenGenerator.YouTube;
using statsd_csharp;

namespace MomentOfZenGenerator
{
    public class Filter : IFilter
    {
        private IYouTubeResponseProvider youTubeResponseProvider;
        private IStatsdClient statsClient;

        public Filter(IYouTubeResponseProvider youTubeResponseProvider, IStatsdClient statsClient)
        {
            this.youTubeResponseProvider = youTubeResponseProvider;
            this.statsClient = statsClient;
        }

        public IEnumerable<String> GetVideoUrlsLessThanOneMinuteLong(String searchWord)
        {
            var videoProjections = youTubeResponseProvider.GetVideos(searchWord);
            var filteredVideos = videoProjections.Where(v => v.Duration <= 60);

            foreach (var video in filteredVideos)
                statsClient.SendTiming("generator.video.duration", video.Duration);

            statsClient.SendCount("generator.video.number-of-results.raw", videoProjections.Count());
            statsClient.SendCount("generator.video.number-of-results.filtered", filteredVideos.Count());

            return filteredVideos.Select<YouTubeVideoProjection, String>(v => v.Url);
        }
    }
}