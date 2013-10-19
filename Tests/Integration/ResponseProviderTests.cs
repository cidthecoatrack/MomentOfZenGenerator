using MomentOfZenGenerator;
using MomentOfZenGenerator.Interfaces;
using NUnit.Framework;
using System;

namespace Tests.Integration
{
    [TestFixture]
    public class ResponseProviderTests
    {
        private IResponseProvider provider;

        [SetUp]
        public void Setup()
        {
            provider = new ResponseProvider();
        }

        [Test]
        public void GoodRequest()
        {
            var content = provider.GetResponseContent("http://www.google.com");
            Assert.That(content, Is.Not.EqualTo(String.Empty));
        }

        [Test, ExpectedException(typeof(UriFormatException))]
        public void BadRequest()
        {
            provider.GetResponseContent("bad uri");
        }
    }
}