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
    }
}
