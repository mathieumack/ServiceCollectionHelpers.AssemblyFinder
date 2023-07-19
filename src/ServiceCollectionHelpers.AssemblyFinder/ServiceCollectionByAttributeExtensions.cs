using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionHelpers.AssemblyFinder.Attributes;
using System;
using System.Reflection;
using System.Linq;

namespace ServiceCollectionHelpers.AssemblyFinder
{
    public static class ServiceCollectionByAttributeExtensions
    {
        /// <summary>
        /// Register all class or interfaces that have the attribute <see cref="ServiceRegisterAttribute"/> defined
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterTypes(this IServiceCollection serviceCollection)
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
    }
}
