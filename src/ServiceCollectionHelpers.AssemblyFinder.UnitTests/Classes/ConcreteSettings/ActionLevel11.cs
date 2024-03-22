using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.Attributes;
using ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Abstractions;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.ConcreteSettings
{
    [ServiceRegister(Scope = ServiceLifetime.Singleton, 
                        ConfigurationKey = "Key1:SubKey1", 
                        RegisterWithInterface = false )]
    internal class ActionLevel11 : IActionAppSettings
    {
        public int Level => 1;
    }
}
