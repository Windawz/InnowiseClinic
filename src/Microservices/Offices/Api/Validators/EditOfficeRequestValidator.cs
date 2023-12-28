using FluentValidation;
using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Requests;

namespace InnowiseClinic.Microservices.Offices.Api.Validators;

public class EditOfficeRequestValidator : AbstractValidator<EditOfficeRequest>
{
    public EditOfficeRequestValidator()
    {
        RuleFor(request => request.City).NotEmpty().Unless(value => value is null);
        RuleFor(request => request.Street).NotEmpty().Unless(value => value is null);
        RuleFor(request => request.HouseNumber).NotEmpty().Unless(value => value is null);
        RuleFor(request => request.OfficeNumber).NotEmpty().Unless(value => value is null);
        RuleFor(request => request.RegistryPhoneNumber).NotEmpty().RegistryPhoneNumber().Unless(value => value is null);
    }
}