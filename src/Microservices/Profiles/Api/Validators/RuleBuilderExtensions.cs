using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Api.Validators;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, int> ValidDoctorStatus<T>(this IRuleBuilder<T, int> builder)
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