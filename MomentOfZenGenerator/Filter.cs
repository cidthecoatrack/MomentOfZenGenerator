using System;
using System.Collections.Generic;
using System.Xml;

namespace MomentOfZenGenerator
{
    public class Filter
    {
        public IEnumerable<String> GetVideoUrlsLessThanOneMinuteLong()
        {
            var requestor = new Requestor(new YouTubeRequestUriBuilder(), new ResponseProvider());
            var content = requestor.GetVideos(String.Empty);

            var videos = new List<String>();

            var videosXml = new XmlDocument();
            videosXml.LoadXml(content);

            var entries = videosXml.SelectNodes("feed/entry");

            foreach (XmlNode entry in entries)
            {
                var duration = Convert.ToInt32(entry.SelectSingleNode("media:group/yt:duration").InnerText);

                if (duration <= 60)
                {
                    var url = entry.SelectSingleNode("media:group/media:player").InnerText;
                    videos.Add(url);
                }
            }

            return videos;
        }
    }
}