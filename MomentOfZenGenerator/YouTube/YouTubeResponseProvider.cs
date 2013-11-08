using System;
using System.Collections.Generic;
using Google.GData.Client;
using Google.GData.YouTube;
using Google.YouTube;
using MomentOfZenGenerator.Interfaces;

namespace MomentOfZenGenerator.YouTube
{
    public class YouTubeResponseProvider : IYouTubeResponseProvider
    {
        private const String apiKey = "AI39si59imK5hP68xR-C4IbraWEIQltv9pSfXouSpTlNESEFzbyhMurwKQV4jWVreZMfjF7zabOBZyDDxKTOEyIuJGh-ChX3og";
        private const String baseYoutubeEmbedUrl = "https://www.youtube.com/embed/";
        private YouTubeRequest request;
        private YouTubeQuery query;

        public YouTubeResponseProvider()
        {
            var settings = new YouTubeRequestSettings("Moment of Zen Generator", apiKey);
            request = new YouTubeRequest(settings);

            InitializeQuery();
        }

        private void InitializeQuery()
        {
            query = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);
            query.SafeSearch = YouTubeQuery.SafeSearchValues.Strict;

            var atomCategory = new AtomCategory("Comedy", YouTubeNameTable.CategorySchema);
            query.Categories.Add(new QueryCategory(atomCategory));
        }

        public IEnumerable<YouTubeVideoProjection> GetVideos(String searchWord)
        {
            query.Query = searchWord;
            var response = request.Get<Video>(query);

            var videos = new List<YouTubeVideoProjection>();

            foreach (var video in response.Entries)
            {
                if (video.Contents.Count > 0)
                {
                    var projection = new YouTubeVideoProjection();

                    projection.Duration = Convert.ToInt32(video.Contents[0].Duration);
                    projection.Url = baseYoutubeEmbedUrl + video.VideoId;

                    videos.Add(projection);
                }
            }

            return videos;
        }
    }
}