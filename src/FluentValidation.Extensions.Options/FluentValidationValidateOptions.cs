using System.Linq;

using Microsoft.Extensions.Options;

namespace FluentValidation.Extensions.Options
{
    public class FluentValidationValidateOptions<TOptions>
        : IValidateOptions<TOptions>
        where TOptions : class
    {
        public FluentValidationValidateOptions(string? name, IValidator<TOptions> validator)
        {
            Name = name;
            Validator = validator;
        }

        public string? Name { get; }

        public IValidator<TOptions> Validator { get; }

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
}
