using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Generics;

[ServiceRegister(Scope = ServiceLifetime.Scoped)]
internal class GenericClass<T>
{
    private readonly SampleValue<T> sampleValue;

    public GenericClass(SampleValue<T> sampleValue)
    {
        this.sampleValue = sampleValue;
    }

    public void Set(T value)
    {
        sampleValue.Value = value;
    }

    public T Get()
    {
        return sampleValue.Value;
    }
}
