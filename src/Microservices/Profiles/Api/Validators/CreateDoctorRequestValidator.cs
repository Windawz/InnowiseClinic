using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;

namespace InnowiseClinic.Microservices.Profiles.Api.Validators;

public class CreateDoctorRequestValidator : AbstractValidator<CreateDoctorRequest>
{
    public CreateDoctorRequestValidator()
    {
        RuleFor(request => request.FirstName).NotEmpty();
        RuleFor(request => request.LastName).NotEmpty();
        RuleFor(request => request.MiddleName).NotEmpty().Unless(request => request.MiddleName is null);
        RuleFor(request => request.CareerStartYear).GreaterThanOrEqualTo(0);
        RuleFor(request => request.Status).ValidDoctorStatus();
    }
}