using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Abstractions;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Tests
{
    [TestClass]
    public class FindAllByInterfaceUnitTests
    {
        [TestMethod]
        public void Register_IAction()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.RegisterClassesOfType<IAction>();

            var serviceProvder = serviceCollection.BuildServiceProvider();

            var services = serviceProvder.GetServices<IAction>();

            Assert.AreEqual(6, services.Count());
        }

        [TestMethod]
        public void Register_IActionNoImpl()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.RegisterClassesOfType<IActionNoImpl>();

            var serviceProvder = serviceCollection.BuildServiceProvider();

            var services = serviceProvder.GetServices<IActionNoImpl>();

            Assert.AreEqual(0, services.Count());
        }

        [TestMethod]
        public void Register_IActionNiImplGetIAction()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.RegisterClassesOfType<IActionNoImpl>();

            var serviceProvder = serviceCollection.BuildServiceProvider();

            var services = serviceProvder.GetServices<IAction>();

            Assert.AreEqual(0, services.Count());
        }
    }
}
