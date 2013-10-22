using MomentOfZenGenerator.YouTube;
using System;
using System.Collections.Generic;

namespace MomentOfZenGenerator.Interfaces
{
    public interface IYouTubeResponseProvider
    {
        IEnumerable<YouTubeVideoProjection> GetVideos(String searchWord);
    }
}