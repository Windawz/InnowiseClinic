using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;

namespace InnowiseClinic.Microservices.Profiles.Api.Validators;

public class EditPatientTargetValidator : AbstractValidator<EditPatientTarget>
{
    public EditPatientTargetValidator()
    {
        RuleFor(target => target.FirstName).NamePart();
        RuleFor(target => target.LastName).NamePart();
        RuleFor(target => target.MiddleName).OptionalNamePart();
        RuleFor(target => target.PhoneNumber).PhoneNumber();
    }
}