using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.Attributes;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Concrete
{
    [ServiceRegister(Scope = ServiceLifetime.Singleton, RegisterWithInterface = true)]
    internal class ActionLevel21 : ActionLevel01
    {
        public override int Level => 2;
    }
}
