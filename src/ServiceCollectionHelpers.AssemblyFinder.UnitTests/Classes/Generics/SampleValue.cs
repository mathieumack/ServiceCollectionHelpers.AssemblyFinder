using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.Attributes;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Generics;

[ServiceRegister(Scope = ServiceLifetime.Scoped)]
internal class SampleValue<T>
{
    public T Value { get; set; }
}
