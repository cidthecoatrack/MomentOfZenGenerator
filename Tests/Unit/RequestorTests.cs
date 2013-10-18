using MomentOfZenGenerator.Interfaces;
using Moq;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class RequestorTests
    {
        [Test]
        public void RequestUriComesFromBuilder()
        {
            var mockRequestUriBuilder = new Mock<IYouTubeRequestUriBuilder>();

        }
    }
}