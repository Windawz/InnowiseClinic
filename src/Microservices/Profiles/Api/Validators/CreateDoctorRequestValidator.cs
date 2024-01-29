using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;

namespace InnowiseClinic.Microservices.Profiles.Api.Validators;

public class CreateDoctorRequestValidator : AbstractValidator<CreateDoctorRequest>
{
    public CreateDoctorRequestValidator()
    {
        RuleFor(request => request.FirstName).NamePart();
        RuleFor(request => request.LastName).NamePart();
        RuleFor(request => request.MiddleName).OptionalNamePart();
        RuleFor(request => request.CareerStartYear).CareerStartYear(request => request.DateOfBirth);
        RuleFor(request => request.Status).DoctorStatus();
    }
}