using FluentValidation;

public static class RuleBuilderExtensions
{
    public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> builder)
    {
        return builder.MinimumLength(6).MaximumLength(15);
    }
}