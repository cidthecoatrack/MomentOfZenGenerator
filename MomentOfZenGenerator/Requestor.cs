using System;
using MomentOfZenGenerator.Interfaces;

namespace MomentOfZenGenerator
{
    public class Requestor
    {
        private IYouTubeRequestUriBuilder requestUriBuilder;
        private IResponseProvider responseProvider;

        public Requestor(IYouTubeRequestUriBuilder requestUriBuilder, IResponseProvider responseProvider)
        {
            this.requestUriBuilder = requestUriBuilder;
            this.responseProvider = responseProvider;
        }

        public String GetVideos(String search)
        {
            var requestUri = requestUriBuilder.BuildRequestUri(search);
            return responseProvider.GetResponseContent(requestUri);
        }
    }
}