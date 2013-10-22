using MomentOfZenGenerator.Interfaces;
using MomentOfZenGenerator.Wordnik;
using Moq;
using NUnit.Framework;
using System;

namespace Tests.Unit
{
    [TestFixture]
    public class WordnikResponseProviderTests
    {
        private IWordnikResponseProvider provider;
        private Mock<IWordnikRequestUriBuilder> mockWordnikRequestUriBuilder;
        private Mock<IResponseProvider> mockResponseProvider;
        private WordnikRandomWordResponse response;

        [SetUp]
        public void Setup()
        {
            mockWordnikRequestUriBuilder = new Mock<IWordnikRequestUriBuilder>();

            response = new WordnikRandomWordResponse();
            mockResponseProvider = new Mock<IResponseProvider>();
            mockResponseProvider.Setup(p => p.GetJsonResponseContent<WordnikRandomWordResponse>(It.IsAny<String>())).Returns(response);

            provider = new WordnikResponseProvider(mockWordnikRequestUriBuilder.Object, mockResponseProvider.Object);
        }

        [Test]
        public void GetTheRandomWordUriFromBuilder()
        {
            provider.GetWord();
            mockWordnikRequestUriBuilder.Verify(b => b.BuildRequestUri(), Times.Once);
        }

        [Test]
        public void GetRandomWordResponseFromResponseProvider()
        {
            provider.GetWord();
            mockResponseProvider.Verify(p => p.GetJsonResponseContent<WordnikRandomWordResponse>(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void GetsJsonResponseFromUriFromBuilder()
        {
            mockWordnikRequestUriBuilder.Setup(b => b.BuildRequestUri()).Returns("random word uri");
            provider.GetWord();
            mockResponseProvider.Verify(p => p.GetJsonResponseContent<WordnikRandomWordResponse>("random word uri"), Times.Once);
        }

        [Test]
        public void ReturnWordFromTheRandomWordResponse()
        {
            response.Word = "random word";
            var word = provider.GetWord();
            Assert.That(word, Is.EqualTo(response.Word));
        }
    }
}