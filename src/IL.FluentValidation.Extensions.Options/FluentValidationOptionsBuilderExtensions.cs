using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IL.FluentValidation.Extensions.Options;

/// <summary>
/// Extension methods for registering FluentValidation validator via <see cref="OptionsBuilder{TOptions}"/>.
/// </summary>
public static class FluentValidationOptionsBuilderExtensions
{
    /// <summary>
    /// Register this options instance for validation of an <see cref="IValidator{T}"/> type.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <typeparam name="TOptionsValidator">The <see cref="IValidator{T}"/> type.</typeparam>
    /// <param name="optionsBuilder">The options builder to add the services to.</param>
    /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that additional calls can be chained.</returns>
    public static OptionsBuilder<TOptions> Validate<TOptions, TOptionsValidator>(
        this OptionsBuilder<TOptions> optionsBuilder
        )
        where TOptions : class
        where TOptionsValidator : class, IValidator<TOptions>
    {
        if (optionsBuilder == null)
        {
            throw new ArgumentNullException(nameof(optionsBuilder));
        }

        optionsBuilder.Services.AddTransient<TOptionsValidator>();
        optionsBuilder.Services.AddTransient<IValidateOptions<TOptions>>(serviceProvider =>
        {
            var validator = serviceProvider.GetRequiredService<TOptionsValidator>();
            return new FluentValidationValidateOptions<TOptions>(optionsBuilder.Name, validator);
        });

        return optionsBuilder;
    }

    /// <summary>
    /// Register this options instance for validation of an <see cref="IValidator{T}"/> instance.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="optionsBuilder">The options builder to add the services to.</param>
    /// <param name="validator">The <see cref="IValidator{T}"/> instance.</param>
    /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that additional calls can be chained.</returns>
    public static OptionsBuilder<TOptions> Validate<TOptions>(
        this OptionsBuilder<TOptions> optionsBuilder,
        IValidator<TOptions> validator
        )
        where TOptions : class
    {
        if (optionsBuilder == null)
        {
            throw new ArgumentNullException(nameof(optionsBuilder));
        }

        if (validator == null)
        {
            throw new ArgumentNullException(nameof(validator));
        }

        optionsBuilder.Services.AddTransient<IValidateOptions<TOptions>>(_ =>
        {
            return new FluentValidationValidateOptions<TOptions>(optionsBuilder.Name, validator);
        });

        return optionsBuilder;
    }

    /// <summary>
    /// Register this options instance for validation of an <see cref="IValidator{T}"/> creator function.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="optionsBuilder">The options builder to add the services to.</param>
    /// <param name="validation">The <see cref="IValidator{T}"/> creator function.</param>
    /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that additional calls can be chained.</returns>
    public static OptionsBuilder<TOptions> Validate<TOptions>(
        this OptionsBuilder<TOptions> optionsBuilder,
        Func<IServiceProvider, IValidator<TOptions>> validation
        )
        where TOptions : class
    {
        if (optionsBuilder == null)
        {
            throw new ArgumentNullException(nameof(optionsBuilder));
        }

        if (validation == null)
        {
            throw new ArgumentNullException(nameof(validation));
        }

        optionsBuilder.Services.AddTransient<IValidateOptions<TOptions>>(serviceProvider =>
        {
            var validator = validation.Invoke(serviceProvider);
            return new FluentValidationValidateOptions<TOptions>(optionsBuilder.Name, validator);
        });

        return optionsBuilder;
    }

    /// <summary>
    /// Register this options instance for validation of an registered <see cref="IValidator{T}"/> in service container.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="optionsBuilder">The options builder to add the services to.</param>
    /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that additional calls can be chained.</returns>
    public static OptionsBuilder<TOptions> ValidateWithFluentValidator<TOptions>(
        this OptionsBuilder<TOptions> optionsBuilder
        )
        where TOptions : class
    {
        if (optionsBuilder == null)
        {
            throw new ArgumentNullException(nameof(optionsBuilder));
        }

        optionsBuilder.Services.AddTransient<IValidateOptions<TOptions>>(serviceProvider =>
        {
            var validator = serviceProvider.GetRequiredService<IValidator<TOptions>>();
            return new FluentValidationValidateOptions<TOptions>(optionsBuilder.Name, validator);
        });

        return optionsBuilder;
    }

    /// <summary>
    /// Fluent style extension methods for registering FluentValidation validator.
    /// </summary>
    /// <typeparam name="TOptions">The options type to be configured.</typeparam>
    /// <param name="optionsBuilder">The options builder to add the services to.</param>
    /// <returns>The wrapper of option builder with fluent style extension methods.</returns>
    public static FluentValidationOptionsBuilderWrapper<TOptions> FluentValidate<TOptions>(this OptionsBuilder<TOptions> optionsBuilder)
        where TOptions : class
        => new(optionsBuilder);
}
