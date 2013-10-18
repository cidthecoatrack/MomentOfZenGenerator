using System;

namespace MomentOfZenGenerator.Interfaces
{
    public interface IYouTubeRequestUriBuilder
    {
        String BuildRequestUri(String search);
    }
}