using FluentValidation;
using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Targets;

namespace InnowiseClinic.Microservices.Offices.Api.Validators;

public class EditOfficeTargetValidator : AbstractValidator<EditOfficeTarget>
{
    public EditOfficeTargetValidator()
    {
        RuleFor(target => target.City).NotEmpty().Unless(value => value is null);
        RuleFor(target => target.Street).NotEmpty().Unless(value => value is null);
        RuleFor(target => target.HouseNumber).NotEmpty().Unless(value => value is null);
        RuleFor(target => target.OfficeNumber).NotEmpty().Unless(value => value is null);
        RuleFor(target => target.RegistryPhoneNumber).NotEmpty().RegistryPhoneNumber().Unless(value => value is null);
    }
}