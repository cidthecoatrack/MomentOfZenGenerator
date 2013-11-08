using System;
using System.Linq;
using MomentOfZenGenerator;
using MomentOfZenGenerator.Interfaces;
using Moq;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class GeneratorTests
    {
        private Generator generator;
        private Mock<Random> mockRandom;
        private Mock<IWordnikResponseProvider> mockWordnikResponseProvider;
        private Mock<IFilter> mockFilter;

        [SetUp]
        public void Setup()
        {
            mockRandom = new Mock<Random>();
            mockWordnikResponseProvider = new Mock<IWordnikResponseProvider>();
            mockFilter = new Mock<IFilter>();
            generator = new Generator(mockRandom.Object, mockWordnikResponseProvider.Object, mockFilter.Object);
        }

        [Test]
        public void GetsWordForSearchFromWordnikResponseProvider()
        {
            generator.GetMomentOfZen();
            mockWordnikResponseProvider.Verify(p => p.GetWord(), Times.Once);
        }

        [Test]
        public void GetVideosFromFilter()
        {
            generator.GetMomentOfZen();
            mockFilter.Verify(f => f.GetVideoUrlsLessThanOneMinuteLong(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void GetVideosFromFitlerWithWordFromWordnik()
        {
            mockWordnikResponseProvider.Setup(p => p.GetWord()).Returns("word");
            generator.GetMomentOfZen();
            mockFilter.Verify(f => f.GetVideoUrlsLessThanOneMinuteLong("word"), Times.Once);
        }

        [Test]
        public void ReturnEmptyStringIfNoVideosFound()
        {
            var momentOfZen = generator.GetMomentOfZen();
            Assert.That(momentOfZen, Is.EqualTo(String.Empty));
        }

        [Test]
        public void GetsVideoFromListByIndexOfRandom()
        {
            var videoUrls = new[] { "first url", "second url", "third url", "fourth url", "url 5" };
            mockFilter.Setup(f => f.GetVideoUrlsLessThanOneMinuteLong(It.IsAny<String>())).Returns(videoUrls);

            generator.GetMomentOfZen();
            mockRandom.Verify(r => r.Next(It.IsAny<Int32>()), Times.Once);
        }

        [Test]
        public void UpperLimitOfRandomIsLengthOfVideos()
        {
            var videoUrls = new[] { "first url", "second url", "third url", "fourth url", "url 5" };
            mockFilter.Setup(f => f.GetVideoUrlsLessThanOneMinuteLong(It.IsAny<String>())).Returns(videoUrls);

            generator.GetMomentOfZen();
            mockRandom.Verify(r => r.Next(videoUrls.Count()), Times.Once);
        }

        [Test]
        public void ReturnsVideoFromIndexDeterminedAtRandom()
        {
            var videoUrls = new[] { "first url", "second url", "third url", "fourth url", "url 5" };
            mockFilter.Setup(f => f.GetVideoUrlsLessThanOneMinuteLong(It.IsAny<String>())).Returns(videoUrls);
            mockRandom.Setup(r => r.Next(It.IsAny<Int32>())).Returns(2);

            var momentOfZen = generator.GetMomentOfZen();
            Assert.That(momentOfZen, Is.EqualTo("third url"));
        }
    }
}