using MomentOfZenGenerator.Interfaces;
using MomentOfZenGenerator.Wordnik;
using Moq;
using NUnit.Framework;
using System;

namespace Tests.Unit
{
    [TestFixture]
    public class WordnikRequestUriBuilderTests
    {
        private Mock<IResponseProvider> mockResponseProvider;
        private IWordnikRequestUriBuilder builder;
        private WordnikFrequencyResponse response;

        [SetUp]
        public void Setup()
        {
            response = new WordnikFrequencyResponse();

            mockResponseProvider = new Mock<IResponseProvider>();
            mockResponseProvider.Setup(p => p.GetJsonResponseContent<WordnikFrequencyResponse>(It.IsAny<String>())).Returns(response);

            builder = new WordnikRequestUriBuilder(mockResponseProvider.Object);
        }

        [Test]
        public void PollsForFrequency()
        {
            builder.BuildRequestUri();
            mockResponseProvider.Verify(p => p.GetJsonResponseContent<WordnikFrequencyResponse>(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void RequestUriHasFrequencyDividedByTwo()
        {
            response.TotalCount = 4;
            var uri = builder.BuildRequestUri();
            Assert.That(uri.Contains("minCorpusCount=2"), Is.True);
        }

        [Test]
        public void RequestUriHasWordnikApiAsRoot()
        {
            var uri = builder.BuildRequestUri();
            Assert.That(uri.StartsWith("http://api.wordnik.com/v4/words.json"), Is.True);
        }

        [Test]
        public void RequestUriHasOtherParameters()
        {
            var uri = builder.BuildRequestUri();
            Assert.That(uri.Contains("excludePartOfSpeech=proper-noun,proper-noun-plural,proper-noun-posessive,suffix,family-name,idiom,affix&hasDictionaryDef=true&includePartOfSpeech=noun,verb,adjective,definite-article,conjunction&limit=26&maxLength=7"), Is.True);
        }

        [Test]
        public void RequestUriHasCorrectApiKey()
        {
            var key = "774294bdb97d07a79400d06796f04c17b6ad2bb70a90c1127";
            var expected = String.Format("api_key={0}", key);

            var uri = builder.BuildRequestUri();
            Assert.That(uri.Contains(expected), Is.True);
        }

        [Test]
        public void RequestUriHasRandomWordsAction()
        {
            var uri = builder.BuildRequestUri();
            Assert.That(uri.Contains("randomWord?"), Is.True);
        }
    }
}