namespace FluentValidation.Extensions.Options.Tests
{
#pragma warning disable CA1710 // Identifiers should have correct suffix
    public class MyOptionsValidator
#pragma warning restore CA1710 // Identifiers should have correct suffix
        : AbstractValidator<MyOptions>
    {
        public MyOptionsValidator()
        {
            RuleFor(x => x.TrueValue).Equal(true);
        }
    }
}
