using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.Attributes;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.ConcreteSettings
{
    [ServiceRegister(Scope = ServiceLifetime.Singleton, ConfigurationKey = "Key1:SubKey2", RegisterWithInterface = true)]
    internal class ActionLevel21 : ActionLevel01
    {
        public override int Level => 2;
    }
}
