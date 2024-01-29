using System.Linq.Expressions;
using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Api.Validators;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string> NamePart<T>(this IRuleBuilder<T, string> builder)
    {
        return builder
            .NotEmpty()
            .Must(namePart => namePart.All(c =>
                char.IsWhiteSpace(c)
                || char.IsLetter(c)
                || char.IsSeparator(c)));
    }

    public static IRuleBuilderOptions<T, string?> OptionalNamePart<T>(this IRuleBuilder<T, string?> builder)
    {
        return builder!.NamePart()
            .Unless(namePart => namePart is null);
    }

    public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> builder)
    {
        return builder.Must(phoneNumber =>
            phoneNumber.All(c =>
                char.IsWhiteSpace(c)
                || char.IsDigit(c)));
    }

    public static IRuleBuilderOptions<T, int> CareerStartYear<T>(this IRuleBuilder<T, int> builder)
    {
        int earliestYear = DateTime.UnixEpoch.Year;
        int currentYear = DateTime.UtcNow.Year;

        return builder.InclusiveBetween(earliestYear, currentYear);
    }

    public static IRuleBuilderOptions<T, int> CareerStartYear<T>(
        this IRuleBuilder<T, int> builder,
        Func<T, DateOnly> dateOfBirthSelector)
    {
        return builder.CareerStartYear()
            .GreaterThan(subject => dateOfBirthSelector(subject).Year)
            .WithMessage("Career start year must be greater than year of birth");
    }

    public static IRuleBuilderOptions<T, int> DoctorStatus<T>(this IRuleBuilder<T, int> builder)
    {
        return builder.Must(status =>
            Enum.IsDefined((DoctorStatus)status))
                .WithMessage("Doctor status must be one of the following values: "
                    + BuildValidDoctorStatusValuesMessageString());
    }

    private static string BuildValidDoctorStatusValuesMessageString()
    {
        var names = Enum.GetNames<DoctorStatus>();
        var values = Enum.GetValues<DoctorStatus>().Select(status => (int)status);
        
        var nameValueStrings = values.Zip(names)
            .Select((value, name) => $"{value} ('{name}')");

        return string.Join(", ", nameValueStrings);
    }
}