using MomentOfZenGenerator;
using MomentOfZenGenerator.Interfaces;
using NUnit.Framework;

namespace Tests.Unit
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
    }
}