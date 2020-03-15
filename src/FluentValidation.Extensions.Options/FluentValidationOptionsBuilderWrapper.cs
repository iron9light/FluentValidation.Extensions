using System;

using Microsoft.Extensions.Options;

namespace FluentValidation.Extensions.Options
{
    public class FluentValidationOptionsBuilderWrapper<TOptions>
        where TOptions : class
    {
        private readonly OptionsBuilder<TOptions> _optionsBuilder;

        public FluentValidationOptionsBuilderWrapper(OptionsBuilder<TOptions> optionsBuilder)
        {
            _optionsBuilder = optionsBuilder;
        }

        public OptionsBuilder<TOptions> With<TOptionsValidator>()
            where TOptionsValidator : class, IValidator<TOptions>
            => _optionsBuilder.Validate<TOptions, TOptionsValidator>();

        public OptionsBuilder<TOptions> With(IValidator<TOptions> validator)
            => _optionsBuilder.Validate(validator);

        public OptionsBuilder<TOptions> With(Func<IServiceProvider, IValidator<TOptions>> validation)
            => _optionsBuilder.Validate(validation);
    }
}
