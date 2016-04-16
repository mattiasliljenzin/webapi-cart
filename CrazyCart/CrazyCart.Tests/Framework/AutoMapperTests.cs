using AutoMapper;
using NUnit.Framework;

namespace CrazyCart.Tests.Framework
{
    [TestFixture]
    public class AutoMapperTests
    {

        [SetUp]
        public void Setup()
        {
            NinjectWebCommon.Start();
        }

        [Test]
        public void Can_verify_mappings()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}
