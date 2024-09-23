using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using System.Linq;
using ServiceCollectionHelpers.AssemblyFinder.Abstractions;
using ServiceCollectionHelpers.AssemblyFinder.Attributes;

namespace ServiceCollectionHelpers.AssemblyFinder;

public static class ServiceCollectionRegisterServicesExtensions
{
    /// <summary>
    /// Search for all IServiceCollectionRegister implementations and call Register method to load services into dependency configurations
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
    {
        var assemblies = ServiceCollectionRegisterTypesExtensions.GetAppDomainAssemblies();

        try
        {
            foreach (var a in assemblies)
            {
                foreach (var t in a.GetTypes())
                {
                    // We have to register the class as it.
                    if (t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(e => e == typeof(IServiceCollectionRegister)))
                    {
                        var instance = (IServiceCollectionRegister)Activator.CreateInstance(t);
                        instance.Register(serviceCollection);
                    }
                }
            }
        }
        catch (ReflectionTypeLoadException ex)
        {
            var msg = string.Join(Environment.NewLine, ex.LoaderExceptions.Select(e => e.Message));
            var fail = new Exception(msg, ex);

            throw fail;
        }

        return serviceCollection;
    }
}
