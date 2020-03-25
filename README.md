# [FluentValidation](https://fluentvalidation.net) integrate with [Microsoft.Extensions](https://github.com/dotnet/extensions)

[![Build Status](https://iron9light.visualstudio.com/github/_apis/build/status/iron9light.FluentValidation.Extensions?branchName=master)](https://iron9light.visualstudio.com/github/_build/latest?definitionId=4&branchName=master)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=iron9light_FluentValidation.Extensions&metric=ncloc)](https://sonarcloud.io/dashboard?id=iron9light_FluentValidation.Extensions)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=iron9light_FluentValidation.Extensions&metric=coverage)](https://sonarcloud.io/dashboard?id=iron9light_FluentValidation.Extensions)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=iron9light_FluentValidation.Extensions&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=iron9light_FluentValidation.Extensions)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=iron9light_FluentValidation.Extensions&metric=security_rating)](https://sonarcloud.io/dashboard?id=iron9light_FluentValidation.Extensions)

## IL.FluentValidation.Extensions.Options

[Microsoft.Extensions.Options](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options#options-validation) validation with [FluentValidation](https://fluentvalidation.net).

[![NuGet](https://img.shields.io/nuget/vpre/IL.FluentValidation.Extensions.Options.svg)](https://www.nuget.org/packages/IL.FluentValidation.Extensions.Options/)

```csharp
public class MyOptionsValidator : AbstractValidator<MyOptions> {
    // ...
}

using IL.FluentValidation.Extensions.Options;

// Registration
services.AddOptions<MyOptions>("optionalOptionsName")
    .Configure(o => { })
    .Validate<MyOptions, MyOptionsValidator>(); // ❗ Register validator type

// Consumption
var monitor = services.BuildServiceProvider()
    .GetService<IOptionsMonitor<MyOptions>>();

try
{
    var options = monitor.Get("optionalOptionsName");
}
catch (OptionsValidationException ex)
{
}
```

Support multiple way to register validator:

```csharp
IValidator<MyOptions> validator = new MyOptionsValidator();
services.AddOptions<MyOptions>("optionalOptionsName")
    .Configure(o => { })
    .Validate(validator); // ❗ Register a validator instance
```

```csharp
IValidator<MyOptions> validator = new MyOptionsValidator();
services.AddTransient<IValidator<MyOptions>, MyOptionsValidator>();
services.AddOptions<MyOptions>("optionalOptionsName")
    .Configure(o => { })
    .Validate(serviceProvider => serviceProvider.GetRequiredService<IValidator<MyOptions>>()); // ❗ Register a validator creator function
```

And your favorite fluent style:

```csharp
IValidator<MyOptions> validator = new MyOptionsValidator();
services.AddOptions<MyOptions>("optionalOptionsName")
    .Configure(o => { })
    .FluentValidate().With<MyOptionsValidator>(); // ❗ Register a validator type
```

```csharp
IValidator<MyOptions> validator = new MyOptionsValidator();
services.AddOptions<MyOptions>("optionalOptionsName")
    .Configure(o => { })
    .FluentValidate().With(validator); // ❗ Register a validator instance
```

```csharp
IValidator<MyOptions> validator = new MyOptionsValidator();
services.AddTransient<IValidator<MyOptions>, MyOptionsValidator>();
services.AddOptions<MyOptions>("optionalOptionsName")
    .Configure(o => { })
    .FluentValidate().With(serviceProvider => serviceProvider.GetRequiredService<IValidator<MyOptions>>()); // ❗ Register a validator creator function
```
