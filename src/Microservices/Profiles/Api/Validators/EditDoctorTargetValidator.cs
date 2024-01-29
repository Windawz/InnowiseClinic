using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;

namespace InnowiseClinic.Microservices.Profiles.Api.Validators;

public class EditDoctorTargetValidator : AbstractValidator<EditDoctorTarget>
{
    public EditDoctorTargetValidator()
    {
        RuleFor(target => target.FirstName).NamePart();
        RuleFor(target => target.LastName).NamePart();
        RuleFor(target => target.MiddleName).OptionalNamePart();
        RuleFor(target => target.CareerStartYear).CareerStartYear(target => target.DateOfBirth);
        RuleFor(target => target.Status).DoctorStatus();
    }
}