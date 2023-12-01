using FluentValidation;

namespace InnowiseClinic.Services.Authorization.API.Services.Validation.Input;

public static class RuleBuilderExtensions
{
    public static IRuleBuilder<T, string> PasswordText<T>(this IRuleBuilder<T, string> builder)
    {
        builder.MinimumLength(8)
            .ForEach(builder => builder.Must(c => char.IsLetterOrDigit(c) || char.IsSeparator(c)));
        return builder;
    }

    public static IRuleBuilder<T, string> RoleName<T>(this IRuleBuilder<T, string> builder)
    {
        builder.MinimumLength(1)
            .ForEach(builder => builder.Must(c => char.IsLetterOrDigit(c)));
        return builder;
    }
}