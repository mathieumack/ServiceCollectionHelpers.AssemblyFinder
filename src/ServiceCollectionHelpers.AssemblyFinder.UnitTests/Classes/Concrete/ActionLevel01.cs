using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.Attributes;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Concrete
{
    [ServiceRegister(Scope = ServiceLifetime.Singleton, RegisterWithInterface = true)]
    internal class ActionLevel01 : AbstractAction
    {
        public virtual int Level => 1;
    }
}
