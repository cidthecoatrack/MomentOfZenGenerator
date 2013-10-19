using MomentOfZenGenerator;
using MomentOfZenGenerator.Interfaces;
using NUnit.Framework;
using System;

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
            var word = responseProvider.GetResponseContent(uri);
            Assert.That(word, Is.Not.EqualTo(String.Empty));
        }
    }
}