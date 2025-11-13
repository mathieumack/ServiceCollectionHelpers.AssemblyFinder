using Microsoft.Extensions.Configuration;
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
            var config = new ConfigurationBuilder()
                .Build();

            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.RegisterTypes(config);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var services = serviceProvider.GetServices<IAction>();
            Assert.AreEqual(2, services.Count());

            var service = serviceProvider.GetServices<ActionLevel11>();
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void RegisterTypesByAttributesFailed()
        {
            var config = new ConfigurationBuilder()
                .Build();

            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.RegisterTypes(config);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            Assert.ThrowsExactly<InvalidOperationException>(() =>
            {
                var service = serviceProvider.GetRequiredService<ActionLevel12>();
            });
        }
    }
}
