using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.Attributes;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.ConcreteSettings
{
    [ServiceRegister(Scope = ServiceLifetime.Singleton, 
                        ConfigurationKey = "Key1:SubKey1",
                        ConfigurationKeyFormat = "true|false",
                        RegisterWithInterface = true)]
    internal class ActionLevel01 : AbstractAction
    {
        public virtual int Level => 1;
    }
}
