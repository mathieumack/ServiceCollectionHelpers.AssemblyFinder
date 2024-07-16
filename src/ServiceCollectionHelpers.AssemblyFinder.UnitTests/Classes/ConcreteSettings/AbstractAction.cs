using ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Abstractions;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.ConcreteSettings
{
    internal abstract class AbstractAction : IActionAppSettings
    {
        public virtual int Level => 0;
    }
}
