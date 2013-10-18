using System;
using MomentOfZenGenerator.Interfaces;

namespace MomentOfZenGenerator
{
    public class YouTubeRequestUriBuilder : IYouTubeRequestUriBuilder
    {
        public String BuildRequestUri(String search)
        {
            var root = "https://gdata.youtube.com/feeds/api/videos";
            var query = String.Format("q={0}", search);
            var getUrlsAndDuration = "fields=entry(media:group(media:player,yt:duration))";
            var getShortDuration = "duration=short";
            var apiVersion = "v=2";

            return String.Format("{0}?{1}&{2}&{3}&{4}", root, query, getUrlsAndDuration, getShortDuration, apiVersion);
        }
    }
}