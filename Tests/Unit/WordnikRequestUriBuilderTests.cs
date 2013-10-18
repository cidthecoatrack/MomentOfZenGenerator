using System;
using MomentOfZenGenerator;
using MomentOfZenGenerator.Interfaces;
using Moq;
using NUnit.Framework;

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
        public void PollsTheWordForFrequency()
        {
            builder.BuildRequestUri();
            mockResponseProvider.Verify(p => p.GetJsonResponseContent<WordnikFrequencyResponse>(It.IsAny<String>()), Times.Once);
        }
    }
}