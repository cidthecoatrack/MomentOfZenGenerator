using System;
using System.Xml;
using MomentOfZenGenerator;
using NUnit.Framework;

namespace Tests.Integration
{
    public class YouTubeRequestUriTests
    {
        private XmlNodeList entries;
        private XmlNamespaceManager namespaceManager;

        [SetUp]
        public void Setup()
        {
            var responseProvider = new ResponseProvider();
            var builder = new YouTubeRequestUriBuilder();
            var uri = builder.BuildRequestUri("video");
            var response = responseProvider.GetResponseContent(uri);

            var videosXml = new XmlDocument();
            videosXml.LoadXml(response);
            entries = videosXml.DocumentElement.ChildNodes;
            namespaceManager = new XmlNamespaceManager(videosXml.NameTable);
            namespaceManager.AddNamespace("yt", "http://gdata.youtube.com/schemas/2007");

        }

        [Test]
        public void ResponseContainsEntries()
        {
            Assert.That(entries.Count, Is.GreaterThan(0));
        }

        [Test]
        public void ResponseContainsVideoDurationAndPlayerUrl()
        {
            foreach (XmlNode entry in entries)
            {
                var attributes = entry.SelectSingleNode("media:group/yt:duration", namespaceManager).Attributes;
                var duration = Convert.ToInt32(attributes["seconds"].InnerText);

                attributes = entry.SelectSingleNode("media:group/media:player", namespaceManager).Attributes;
                var url = attributes["url"].InnerText;

                Assert.That(duration, Is.Not.EqualTo(0));
                Assert.That(url, Is.Not.EqualTo(String.Empty));
            }
        }

        [Test]
        public void ResponseDoesNotContainAnyOtherNodes()
        {
            foreach (XmlNode entry in entries)
            {
                Assert.That(entry.ChildNodes.Count, Is.EqualTo(1));

                var mediaGroup = entry.SelectSingleNode("media:group", namespaceManager);
                Assert.That(mediaGroup.ChildNodes.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void ResponseVideoUrlIsUrlToYouTubeVideo()
        {
            foreach (XmlNode entry in entries)
            {
                var attributes = entry.SelectSingleNode("media:group/media:player", namespaceManager).Attributes;
                var url = attributes["url"].InnerText;
                Assert.That(url.StartsWith("http://www.youtube.com/watch?v="), Is.True);
            }
        }
    }
}