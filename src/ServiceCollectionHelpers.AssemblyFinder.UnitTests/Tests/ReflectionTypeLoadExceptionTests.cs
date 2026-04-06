using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Tests
{
    [TestClass]
    public class ReflectionTypeLoadExceptionTests
    {
        [TestMethod]
        public void RegisterClassesOfType_DoesNotThrowWithProblematicAssemblies()
        {
            // This test verifies that RegisterClassesOfType doesn't throw exceptions
            // when scanning assemblies that have types that can't be loaded.
            // This simulates the issue with Azure.AI.OpenAI and similar assemblies.
            
            IServiceCollection serviceCollection = new ServiceCollection();

            // This should complete without throwing a ReflectionTypeLoadException
            // even if some assemblies in the AppDomain have types that can't be loaded
            try
            {
                serviceCollection.RegisterClassesOfType<IDisposable>();
                // If we get here, the test passed
                Assert.IsTrue(true);
            }
            catch (ReflectionTypeLoadException)
            {
                Assert.Fail("RegisterClassesOfType threw ReflectionTypeLoadException");
            }
        }

        [TestMethod]
        public void RegisterTypesByAttribute_DoesNotThrowWithProblematicAssemblies()
        {
            // This test verifies that RegisterTypes (attribute-based) doesn't throw exceptions
            // when scanning assemblies that have types that can't be loaded.
            
            IServiceCollection serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            // This should complete without throwing a ReflectionTypeLoadException
            // even if some assemblies in the AppDomain have types that can't be loaded
            try
            {
                serviceCollection.RegisterTypes(configuration);
                // If we get here, the test passed
                Assert.IsTrue(true);
            }
            catch (ReflectionTypeLoadException)
            {
                Assert.Fail("RegisterTypes threw ReflectionTypeLoadException");
            }
        }
    }
}
