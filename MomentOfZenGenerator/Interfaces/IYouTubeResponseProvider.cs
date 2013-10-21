using System;
using Google.GData.Client;
using Google.YouTube;

namespace MomentOfZenGenerator.Interfaces
{
    public interface IYouTubeResponseProvider
    {
        Feed<Video> GetVideos(String searchWord);
    }
}