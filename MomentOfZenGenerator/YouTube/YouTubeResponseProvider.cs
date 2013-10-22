using Google.GData.YouTube;
using Google.YouTube;
using MomentOfZenGenerator.Interfaces;
using System;
using System.Collections.Generic;

namespace MomentOfZenGenerator.YouTube
{
    public class YouTubeResponseProvider : IYouTubeResponseProvider
    {
        private const String apiKey = "AI39si59imK5hP68xR-C4IbraWEIQltv9pSfXouSpTlNESEFzbyhMurwKQV4jWVreZMfjF7zabOBZyDDxKTOEyIuJGh-ChX3og";
        private YouTubeRequest request;
        private YouTubeQuery query;

        public YouTubeResponseProvider()
        {
            var settings = new YouTubeRequestSettings("Moment of Zen Generator", apiKey);
            request = new YouTubeRequest(settings);

            query = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);
            query.SafeSearch = YouTubeQuery.SafeSearchValues.Moderate;
        }

        public IEnumerable<YouTubeVideoProjection> GetVideos(String searchWord)
        {
            query.Query = searchWord;
            var response = request.Get<Video>(query);

            var videos = new List<YouTubeVideoProjection>();

            foreach (var video in response.Entries)
            {
                var projection = new YouTubeVideoProjection();

                projection.Duration = Convert.ToInt32(video.Contents[0].Duration);
                projection.Url = video.Contents[0].Url;

                videos.Add(projection);
            }

            return videos;
        }
    }
}