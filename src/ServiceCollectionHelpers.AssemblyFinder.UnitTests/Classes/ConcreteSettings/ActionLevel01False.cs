using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.Attributes;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.ConcreteSettings
{
    [ServiceRegister(Scope = ServiceLifetime.Singleton, 
                        ConfigurationKey = "Key1:SubKey1",
                        ConfigurationKeyFormat = "[a-z]+",
                        RegisterWithInterface = true)]
    internal class ActionLevel01False : AbstractAction
    {
        public virtual int Level => 1;
    }
}
