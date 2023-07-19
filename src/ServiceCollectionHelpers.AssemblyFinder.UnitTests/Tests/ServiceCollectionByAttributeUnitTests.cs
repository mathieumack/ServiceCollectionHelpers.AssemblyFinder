using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Abstractions;
using ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Concrete;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Tests
{
    [TestClass]
    public class ServiceCollectionByAttributeUnitTests
    {
        [TestMethod]
        public void RegisterTypesByAttributes()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.RegisterTypes();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var services = serviceProvider.GetServices<IAction>();
            Assert.AreEqual(2, services.Count());

            var service = serviceProvider.GetServices<ActionLevel11>();
            Assert.IsNotNull(service);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterTypesByAttributesFailed()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.RegisterTypes();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<ActionLevel12>();
        }
    }
}
