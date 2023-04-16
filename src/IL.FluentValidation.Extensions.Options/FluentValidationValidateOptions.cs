using FluentValidation;

using Microsoft.Extensions.Options;

namespace IL.FluentValidation.Extensions.Options;

/// <summary>
/// Implementation of <see cref="IValidateOptions{TOptions}"/> that uses FluentValidation's <see cref="IValidator{T}"/> for validation.
/// </summary>
/// <typeparam name="TOptions">The instance being validated.</typeparam>
public class FluentValidationValidateOptions<TOptions>
    : IValidateOptions<TOptions>
    where TOptions : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentValidationValidateOptions{TOptions}"/> class.
    /// Constructor.
    /// </summary>
    /// <param name="name">The name of the option.</param>
    /// <param name="validator">The <see cref="IValidator{T}"/> validator.</param>
    public FluentValidationValidateOptions(string? name, IValidator<TOptions> validator)
    {
        Name = name;
        Validator = validator;
    }

    /// <summary>
    /// Gets the options name.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Gets the <see cref="IValidator{T}"/> validator.
    /// </summary>
    public IValidator<TOptions> Validator { get; }

    /// <inheritdoc/>
    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (Name == null || name == Name)
        {
            var validationResult = Validator.Validate(options);
            if (validationResult.IsValid)
            {
                return ValidateOptionsResult.Success;
            }
            else
            {
                return ValidateOptionsResult.Fail(
                    validationResult.Errors.Select(error => error.ToString())
                    );
            }
        }

        return ValidateOptionsResult.Skip;
    }
}
