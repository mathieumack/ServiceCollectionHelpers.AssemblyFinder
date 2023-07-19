using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.Attributes;
using ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Abstractions;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Concrete
{
    [ServiceRegister(Scope = ServiceLifetime.Singleton, RegisterWithInterface = false )]
    internal class ActionLevel11 : IAction
    {
        public int Level => 1;
    }
}
