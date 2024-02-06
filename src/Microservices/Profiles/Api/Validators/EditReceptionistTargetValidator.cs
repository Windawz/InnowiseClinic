using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;

namespace InnowiseClinic.Microservices.Profiles.Api.Validators;

public class EditReceptionistTargetValidator : AbstractValidator<EditReceptionistTarget>
{
    public EditReceptionistTargetValidator()
    {
        RuleFor(target => target.FirstName).NotEmpty();
        RuleFor(target => target.LastName).NotEmpty();
        RuleFor(target => target.MiddleName).NotEmpty().Unless(request => request.MiddleName is null);
    }
}