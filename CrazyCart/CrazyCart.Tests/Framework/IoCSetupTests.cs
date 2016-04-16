using System;
using System.Linq;
using System.Reflection;
using CrazyCart.Setup;
using CrazyCartServiceContracts.Contracts;
using Ninject;
using NUnit.Framework;
using Shouldly;

namespace CrazyCart.Tests.Framework
{
    [TestFixture]
    public class IoCSetupTests
    {

        [SetUp]
        public void Setup()
        {
            NinjectWebCommon.Start();
        }

        [Test]
        public void Can_retrieve_service_contracts_implementations()
        {
            // Arrange

            // Act & Assert
            var serviceContracts = Assembly.GetAssembly(typeof (ICartStorage));
            var interfaces = serviceContracts.GetTypes().Where(x => x.IsInterface && x != typeof(IStorage)).ToList();

            interfaces.ForEach(x =>
            {
                var type = serviceContracts.GetType(x.FullName);
                System.Diagnostics.Debug.WriteLine(string.Format("Trying to resolve service: {0}", type.Name));

                var implementation = DependecyResolver.Resolve(type);
                implementation.ShouldNotBe(null);
                System.Diagnostics.Debug.WriteLine(string.Format("Found matching implementation: {0}", implementation.GetType().Name));
            });

        }
    }
}
