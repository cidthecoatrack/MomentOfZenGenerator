using System;
using System.Collections.Generic;
using MomentOfZenGenerator.Interfaces;

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
            var searchResults = youTubeResponseProvider.GetVideos(searchWord);
            var videoUrls = new List<String>();

            foreach (var video in searchResults.Entries)
            {
                var mediaContent = video.Contents[0];
                var duration = Convert.ToInt32(mediaContent.Duration);

                if (duration <= 60)
                    videoUrls.Add(mediaContent.Url);
            }

            return videoUrls;
        }
    }
}