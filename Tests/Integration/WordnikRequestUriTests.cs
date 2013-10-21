using System;
using MomentOfZenGenerator;
using MomentOfZenGenerator.Interfaces;
using MomentOfZenGenerator.Wordnik;
using NUnit.Framework;

namespace Tests.Integration
{
    [TestFixture]
    public class WordnikRequestUriTests
    {
        private IResponseProvider responseProvider;
        private String uri;

        [SetUp]
        public void Setup()
        {
            responseProvider = new ResponseProvider();
            var builder = new WordnikRequestUriBuilder(responseProvider);
            uri = builder.BuildRequestUri();
        }

        [Test]
        public void GetARandomWordBack()
        {
            var randomWordResponse = responseProvider.GetJsonResponseContent<WordnikRandomWordResponse>(uri);
            Assert.That(randomWordResponse.Word, Is.Not.EqualTo(String.Empty));
        }
    }
}