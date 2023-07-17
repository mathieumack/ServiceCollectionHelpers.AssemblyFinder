using System; 
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ServiceCollectionHelpers.AssemblyFinder
{
    public class RegisterAsOptions
    {
        /// <summary>
        /// Indicate the type of register in the service collection
        /// </summary>
        public RegisterAs RegisterAs { get; set; } = RegisterAs.Transient;

        ///// <summary>
        ///// Limit register to only concrete class, not abstract
        ///// </summary>
        //public bool RegisterOnlyConcreteClass { get; set; } = true;

        public List<Assembly> Assemblies { get; } = new List<Assembly>();
    }
}
