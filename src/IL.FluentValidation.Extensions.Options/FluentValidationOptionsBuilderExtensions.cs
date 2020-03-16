using System;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IL.FluentValidation.Extensions.Options
{
    public static class FluentValidationOptionsBuilderExtensions
    {
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

        public static FluentValidationOptionsBuilderWrapper<TOptions> FluentValidate<TOptions>(this OptionsBuilder<TOptions> optionsBuilder)
            where TOptions : class
            => new FluentValidationOptionsBuilderWrapper<TOptions>(optionsBuilder);
    }
}
