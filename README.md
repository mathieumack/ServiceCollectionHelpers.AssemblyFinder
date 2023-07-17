# ServiceCollectionHelpers.AssemblyFinder
This package contains helpers to find assemblies class or interface and register them in the dependency injection container.

==========

# Onboarding Instructions 

## Installation

1. Add nuget package: 

> Install-Package ServiceCollectionHelpers.AssemblyFinder

2. In your application, you can call the extension RegisterClassesOfType to find and register some classes or interfaces :

```csharp
    builder.Services.RegisterClassesOfType<ISomeInterface>();
```

In case of the type is a class, it will register all classes that inherits from the type. In case of the type is an interface, it will register all classes that implements the interface.

By default, all register classes are registered as Transient. But you can override this configuration by using the overload of the method :

```csharp
    builder.Services.RegisterClassesOfType<ISomeInterface>(new RegisterAsOptions()
    {
        RegisterAs = RegisterAs.Scoped
    });
```

You can find more details and samples in the Wiki or in unit tests.

# IC
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=mathieumack_ServiceCollectionHelpers.AssemblyFinder&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=mathieumack_ServiceCollectionHelpers.AssemblyFinder)
[![.NET](https://github.com/mathieumack/ServiceCollectionHelpers.AssemblyFinder/actions/workflows/ci.yml/badge.svg)](https://github.com/mathieumack/ServiceCollectionHelpers.AssemblyFinder/actions/workflows/ci.yml)
[![NuGet package](https://buildstats.info/nuget/ServiceCollectionHelpers.AssemblyFinder?includePreReleases=true)](https://nuget.org/packages/ServiceCollectionHelpers.AssemblyFinder)

# Documentation : I want more

Do not hesitate to check unit tests on the solution. It's a good way to check how transformations are tested.

Also, to get more samples, go to the [Wiki](https://github.com/mathieumack/ServiceCollectionHelpers.AssemblyFinder/wiki). 

Do not hesitate to contribute.


# Support / Contribute
> If you have any questions, problems or suggestions, create an issue or fork the project and create a Pull Request.
