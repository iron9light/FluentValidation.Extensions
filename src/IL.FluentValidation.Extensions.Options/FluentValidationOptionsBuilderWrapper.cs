using FluentValidation;

using Microsoft.Extensions.Options;

namespace IL.FluentValidation.Extensions.Options;

/// <summary>
/// Wrapper class of <see cref="OptionsBuilder{TOptions}"/> with fluent style extension methods for registering FluentValidation validator.
/// </summary>
/// <typeparam name="TOptions">The option type being validated.</typeparam>
public class FluentValidationOptionsBuilderWrapper<TOptions>
    where TOptions : class
{
    private readonly OptionsBuilder<TOptions> _optionsBuilder;

    /// <summary>
    /// Initializes a new instance of the <see cref="FluentValidationOptionsBuilderWrapper{TOptions}"/> class.
    /// </summary>
    /// <param name="optionsBuilder">The options builder.</param>
    public FluentValidationOptionsBuilderWrapper(OptionsBuilder<TOptions> optionsBuilder)
    {
        _optionsBuilder = optionsBuilder;
    }

    /// <summary>
    /// Register this options instance for validation of an <see cref="IValidator{T}"/> type.
    /// </summary>
    /// <typeparam name="TOptionsValidator">The <see cref="IValidator{T}"/> type.</typeparam>
    /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that additional calls can be chained.</returns>
    public OptionsBuilder<TOptions> With<TOptionsValidator>()
        where TOptionsValidator : class, IValidator<TOptions>
        => _optionsBuilder.Validate<TOptions, TOptionsValidator>();

    /// <summary>
    /// Register this options instance for validation of an <see cref="IValidator{T}"/> instance.
    /// </summary>
    /// <param name="validator">The <see cref="IValidator{T}"/> instance.</param>
    /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that additional calls can be chained.</returns>
    public OptionsBuilder<TOptions> With(IValidator<TOptions> validator)
        => _optionsBuilder.Validate(validator);

    /// <summary>
    /// Register this options instance for validation of an <see cref="IValidator{T}"/> creator function.
    /// </summary>
    /// <param name="validation">The <see cref="IValidator{T}"/> creator function.</param>
    /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that additional calls can be chained.</returns>
    public OptionsBuilder<TOptions> With(Func<IServiceProvider, IValidator<TOptions>> validation)
        => _optionsBuilder.Validate(validation);
}
