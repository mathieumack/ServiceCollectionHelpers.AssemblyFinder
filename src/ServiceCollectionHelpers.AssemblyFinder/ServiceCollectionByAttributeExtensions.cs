using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.Attributes;
using System;
using System.Reflection;
using System.Linq;
using System.Configuration;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;

namespace ServiceCollectionHelpers.AssemblyFinder
{
    public static class ServiceCollectionByAttributeExtensions
    {
        /// <summary>
        /// Register all class or interfaces that have the attribute <see cref="ServiceRegisterAttribute"/> defined
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterTypes(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var assemblies = ServiceCollectionExtensions.GetAppDomainAssemblies();

            try
            {
                foreach (var a in assemblies)
                {
                    foreach (var t in a.GetTypes())
                    {
                        var attribute = t.GetCustomAttribute(typeof(ServiceRegisterAttribute));
                        if (!t.IsInterface)
                        {
                            // We have to register the class as it.
                            if (t.IsClass && !t.IsAbstract && attribute != null)
                            {
                                var serviceRegisterAttribute = attribute as ServiceRegisterAttribute;

                                // Test configuration key and format (if provided)
                                if (!string.IsNullOrEmpty(serviceRegisterAttribute.ConfigurationKey))
                                {
                                    if (string.IsNullOrEmpty(serviceRegisterAttribute.ConfigurationKeyFormat))
                                    {
                                        var variableValue = serviceRegisterAttribute.ConfigurationKey.GetAppSettingsValue(configuration);
                                        if (string.IsNullOrEmpty(variableValue))
                                            continue;
                                    }
                                    else
                                    {
                                        var variableValue = serviceRegisterAttribute.ConfigurationKey.GetAppSettingsValue(configuration);
                                        if (string.IsNullOrEmpty(variableValue))
                                            continue;

                                        var regex = new System.Text.RegularExpressions.Regex(serviceRegisterAttribute.ConfigurationKeyFormat);
                                        if (!regex.IsMatch(variableValue))
                                            continue;
                                    }
                                }

                                var registeroption = new RegisterAsOptions()
                                {
                                    ServiceLifetime = serviceRegisterAttribute.Scope
                                };

                                if (!serviceRegisterAttribute.RegisterWithInterface)
                                    serviceCollection.Register(t, t, registeroption);
                                else
                                {
                                    foreach(var interfaceType in t.GetInterfaces())
                                    {
                                        serviceCollection.Register(t, interfaceType, registeroption);
                                    }
                                }
                            }
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
}
