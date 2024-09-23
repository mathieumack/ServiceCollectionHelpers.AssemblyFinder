using Microsoft.Extensions.DependencyInjection;

namespace ServiceCollectionHelpers.AssemblyFinder.Abstractions;

public interface IServiceCollectionRegister
{
    /// <summary>
    /// Condition to register the service. The provided key must exists in the configuration
    /// </summary>
    public string ConfigurationKey { get; }

    /// <summary>
    /// Regex to match the configuration key (if configuration key is provided and founded)
    /// If this field is null or empty, it's equivalent to test if the configuration key is not null or empty
    /// </summary>
    public string ConfigurationKeyFormat { get; }

    public void Register(IServiceCollection services);
}
