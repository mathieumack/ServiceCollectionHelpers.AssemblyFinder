using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Abstractions;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Tests
{
    [TestClass]
    public class LoadByAppSettingsTests
    {
        [TestMethod]
        public void Register_ExistsInAppSettings()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings1.json")
                .Build();

            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.RegisterTypes(config);

            var serviceProvder = serviceCollection.BuildServiceProvider();

            var services = serviceProvder.GetServices<IActionAppSettings>();

            var allServices = services.ToList();

            Assert.AreEqual(3, allServices.Count);
        }

        [TestMethod]
        public void Register_DontExistsInAppSettings()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings2.json")
                .Build();

            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.RegisterTypes(config);

            var serviceProvder = serviceCollection.BuildServiceProvider();

            var services = serviceProvder.GetServices<IActionAppSettings>();

            var allServices = services.ToList();

            Assert.AreEqual(1, allServices.Count);
        }
    }
}
