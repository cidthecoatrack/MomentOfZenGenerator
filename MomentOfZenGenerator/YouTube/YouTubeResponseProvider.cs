using System;
using Google.GData.Client;
using Google.GData.YouTube;
using Google.YouTube;
using MomentOfZenGenerator.Interfaces;

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

        public Feed<Video> GetVideos(String searchWord)
        {
            query.Query = searchWord;
            return request.Get<Video>(query);
        }
    }
}