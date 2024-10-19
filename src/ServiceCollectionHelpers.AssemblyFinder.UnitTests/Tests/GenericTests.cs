using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Abstractions;
using ServiceCollectionHelpers.AssemblyFinder.UnitTests.Classes.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCollectionHelpers.AssemblyFinder.UnitTests.Tests;

[TestClass]
public class GenericTests
{
    [TestMethod]
    public void Register_ScopedGenericClass()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings1.json")
            .Build();

        IServiceCollection serviceCollection = new ServiceCollection();

        serviceCollection.RegisterTypes(config);

        var serviceProvder = serviceCollection.BuildServiceProvider();

        var intService = serviceProvder.GetRequiredService<GenericClass<int>>();
        var stringService = serviceProvder.GetRequiredService<GenericClass<string>>();

        intService.Set(1);
        stringService.Set("test");

        var intServiceScoped = serviceProvder.GetRequiredService<GenericClass<int>>();
        var stringServiceScoped = serviceProvder.GetRequiredService<GenericClass<string>>();

        Assert.AreEqual(1, intService.Get());
        Assert.AreEqual(1, intServiceScoped.Get());
        Assert.AreEqual("test", stringService.Get());
        Assert.AreEqual("test", stringServiceScoped.Get());


        intService.Set(10);
        Assert.AreEqual(10, intService.Get());
        Assert.AreEqual(10, intServiceScoped.Get());
    }
}
