using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using System.Linq;
using ServiceCollectionHelpers.AssemblyFinder.Abstractions;
using Microsoft.Extensions.Configuration;

namespace ServiceCollectionHelpers.AssemblyFinder;

public static class ServiceCollectionRegisterServicesExtensions
{
    /// <summary>
    /// Search for all IServiceCollectionRegister implementations and call Register method to load services into dependency configurations
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection, IConfiguration configuration)
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

                        if (!string.IsNullOrEmpty(instance.ConfigurationKey))
                        {
                            if (string.IsNullOrEmpty(instance.ConfigurationKeyFormat))
                            {
                                var variableValue = instance.ConfigurationKey.GetAppSettingsValue(configuration);
                                if (string.IsNullOrEmpty(variableValue))
                                    continue;
                            }
                            else
                            {
                                var variableValue = instance.ConfigurationKey.GetAppSettingsValue(configuration);
                                if (string.IsNullOrEmpty(variableValue))
                                    continue;

                                var regex = new System.Text.RegularExpressions.Regex(instance.ConfigurationKeyFormat, System.Text.RegularExpressions.RegexOptions.IgnoreCase, new TimeSpan(0, 0, 5));
                                if (!regex.IsMatch(variableValue))
                                    continue;
                            }
                        }

                        instance.Register(serviceCollection, configuration);
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
    private static string GetAppSettingsValue(this string variableKey, IConfiguration configuration)
    {
        return configuration.GetSection(variableKey).Value;
    }
}
