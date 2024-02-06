using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;

namespace InnowiseClinic.Microservices.Profiles.Api.Validators;

public class EditPatientTargetValidator : AbstractValidator<EditPatientTarget>
{
    public EditPatientTargetValidator()
    {
        RuleFor(target => target.FirstName).NotEmpty();
        RuleFor(target => target.LastName).NotEmpty();
        RuleFor(target => target.MiddleName).NotEmpty().Unless(request => request.MiddleName is null);
        RuleFor(target => target.PhoneNumber).NotEmpty();
    }
}