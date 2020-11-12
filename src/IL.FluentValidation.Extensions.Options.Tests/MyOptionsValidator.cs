using FluentValidation;

namespace IL.FluentValidation.Extensions.Options.Tests
{
    public class MyOptionsValidator
        : AbstractValidator<MyOptions>
    {
        public MyOptionsValidator()
        {
            RuleFor(x => x.TrueValue).Equal(true);
        }
    }
}
