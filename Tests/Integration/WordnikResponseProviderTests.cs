using MomentOfZenGenerator;
using MomentOfZenGenerator.Interfaces;
using MomentOfZenGenerator.Wordnik;
using NUnit.Framework;
using System;

namespace Tests.Integration
{
    [TestFixture]
    public class WordnikResponseProviderTests
    {
        private IWordnikResponseProvider provider;
        private String uri;

        [SetUp]
        public void Setup()
        {
            var responseProvider = new ResponseProvider();
            var builder = new WordnikRequestUriBuilder(responseProvider);
            provider = new WordnikResponseProvider(builder, responseProvider);
        }

        [Test]
        public void GetARandomWordBack()
        {
            var randomWord = provider.GetWord();
            Assert.That(randomWord, Is.Not.EqualTo(String.Empty));
        }
    }
}