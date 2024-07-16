using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServiceCollectionHelpers.AssemblyFinder.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ServiceRegisterAttribute : Attribute
    {
        /// <summary>
        /// If True, the class will be registered with thse interfaces.
        /// If Fals, the class will be registerd as self
        /// </summary>
        public bool RegisterWithInterface { get; set; }

        /// <summary>
        /// Lifetime scope (Transient, Scoped or Singleton)
        /// </summary>
        public ServiceLifetime Scope { get; set; }

        /// <summary>
        /// Condition to register the service. The provided key must exists in the configuration
        /// </summary>
        public string ConfigurationKey { get; set; }
        
        /// <summary>
        /// Regex to match the configuration key (if configuration key is provided and founded)
        /// If this field is null or empty, it's equivalent to test if the configuration key is not null or empty
        /// </summary>
        public string ConfigurationKeyFormat { get; set; }
    }
}
