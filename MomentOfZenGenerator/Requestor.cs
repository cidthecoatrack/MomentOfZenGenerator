using System;
using System.IO;
using System.Net;
using MomentOfZenGenerator.Interfaces;

namespace MomentOfZenGenerator
{
    public class Requestor
    {
        private IRequestUriBuilder requestUriBuilder;

        public Requestor(IRequestUriBuilder requestUriBuilder)
        {
            this.requestUriBuilder = requestUriBuilder;
        }

        public String GetVideos(String search)
        {
            var requestUrl = requestUriBuilder.BuildRequestUrl(search);
            return GetResponse(requestUrl);
        }

        private String GetResponse(String uri)
        {
            var request = WebRequest.Create(uri);

            var response = request.GetResponse();
            var content = String.Empty;

            using (var reader = new StreamReader(response.GetResponseStream()))
                content = reader.ReadToEnd();

            return content;
        }
    }
}