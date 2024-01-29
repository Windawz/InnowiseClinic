using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;

namespace InnowiseClinic.Microservices.Profiles.Api.Validators;

public class EditReceptionistTargetValidator : AbstractValidator<EditReceptionistTarget>
{
    public EditReceptionistTargetValidator()
    {
        RuleFor(target => target.FirstName).NamePart();
        RuleFor(target => target.LastName).NamePart();
        RuleFor(target => target.MiddleName).OptionalNamePart();
    }
}