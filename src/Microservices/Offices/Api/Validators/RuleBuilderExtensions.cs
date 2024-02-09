using FluentValidation;

namespace InnowiseClinic.Microservices.Offices.Api.Validators;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string?> RegistryPhoneNumber<T>(this IRuleBuilder<T, string?> builder)
    {
        return builder.Must(value => value is not null && value.All(c => char.IsDigit(c)));
    }
}