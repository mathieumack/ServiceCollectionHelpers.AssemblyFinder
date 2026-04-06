using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ServiceCollectionHelpers.AssemblyFinder;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterTypes<T>(this IServiceCollection serviceCollection)
    {
        return serviceCollection.RegisterClassesOfType(typeof(T), new RegisterAsOptions()
        {
            ServiceLifetime = ServiceLifetime.Scoped
        });
    }

    public static IServiceCollection RegisterClassesOfType<T>(this IServiceCollection serviceCollection, RegisterAsOptions options = null)
    {
        return serviceCollection.RegisterClassesOfType(typeof(T), options ?? new RegisterAsOptions()
        {
            ServiceLifetime = ServiceLifetime.Scoped
        });
    }

    public static IServiceCollection RegisterClassesOfType(this IServiceCollection serviceCollection, Type assignTypeFrom, RegisterAsOptions options = null)
    {
        // Add default assemblies :
        if(!options.Assemblies.Any())
            options.Assemblies.AddRange(GetAppDomainAssemblies());

        if(assignTypeFrom.IsInterface)
        {
            // We have to find all classes that implements this one
            return serviceCollection.RegisterClassesThatInheritsInterface(assignTypeFrom, options);
        }
        else
        {
            // We have to find all classes that inherits from this one
            return serviceCollection.RegisterClassesThatInheritsFrom(assignTypeFrom, options);
        }
    }

    private static IServiceCollection RegisterClassesThatInheritsInterface(this IServiceCollection serviceCollection, Type interfaceType, RegisterAsOptions options = null)
    {
        foreach (var a in options.Assemblies)
        {
            foreach (var t in GetLoadableTypes(a))
            {
                if (!t.IsInterface && t.GetInterfaces().Contains(interfaceType))
                {
                    if (t.IsClass && !t.IsAbstract)
                    {
                        serviceCollection.Register(t, interfaceType, options);
                    }
                }
            }
        }

        return serviceCollection;
    }

    internal static IServiceCollection RegisterClassesThatInheritsFrom(this IServiceCollection serviceCollection, Type inheritsType, RegisterAsOptions options = null)
    {
        var result = new List<Type>();
        
        foreach (var a in options.Assemblies)
        {
            foreach (var t in GetLoadableTypes(a))
            {
                if (!t.IsInterface && t.GetNestedTypes().Contains(inheritsType))
                {
                    if (t.IsClass && !t.IsAbstract)
                    {
                        result.Add(t);
                    }
                }
            }
        }

        serviceCollection.Register(result, inheritsType, options);

        return serviceCollection;
    }

    internal static void Register(this IServiceCollection serviceCollection, List<Type> types, Type serviceType, RegisterAsOptions options)
    {
        foreach (var type in types)
        {
            serviceCollection.Register(type, serviceType, options);
        }
    }

    internal static void Register(this IServiceCollection serviceCollection, Type type, Type serviceType, RegisterAsOptions options)
    {
        if (options.ServiceLifetime == ServiceLifetime.Transient)
            serviceCollection.AddTransient(serviceType, type);
        else if (options.ServiceLifetime == ServiceLifetime.Scoped)
            serviceCollection.AddScoped(serviceType, type);
        else if (options.ServiceLifetime == ServiceLifetime.Singleton)
            serviceCollection.AddSingleton(serviceType, type);
        else
            throw new InvalidOperationException("The RegisterAs option is not recongnised as a valid type. Supported :Transient, Scoped, Singleton.");
    }

    /// <summary>
    /// Gets tne assemblies related to the current implementation.
    /// </summary>
    /// <returns>A list of assemblies that should be loaded by the Nop factory.</returns>
    internal static IList<Assembly> GetAppDomainAssemblies()
    {
        return AppDomain.CurrentDomain.GetAssemblies().OrderBy(e => e.FullName).ToList();
    }

    #region Utilities

    /// <summary>
    /// Safely gets all loadable types from an assembly.
    /// If a ReflectionTypeLoadException occurs, returns only the types that could be loaded.
    /// </summary>
    /// <param name="assembly">The assembly to get types from</param>
    /// <returns>An array of types that could be successfully loaded</returns>
    internal static Type[] GetLoadableTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            // Return only the types that were successfully loaded
            return ex.Types.Where(t => t != null).ToArray();
        }
    }

    /// <summary>
    /// Does type implement generic?
    /// </summary>
    /// <param name="type"></param>
    /// <param name="openGeneric"></param>
    /// <returns></returns>
    internal static bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
    {
        try
        {
            var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
            foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
            {
                if (!implementedInterface.IsGenericType)
                    continue;

                var isMatch = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                return isMatch;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    #endregion
}
