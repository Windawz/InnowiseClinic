using FluentValidation;
using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Requests;

namespace InnowiseClinic.Microservices.Offices.Api.Validators;

public class CreateOfficeRequestValidator : AbstractValidator<CreateOfficeRequest>
{
    public CreateOfficeRequestValidator()
    {
        RuleFor(request => request.City).NotEmpty();
        RuleFor(request => request.Street).NotEmpty();
        RuleFor(request => request.HouseNumber).NotEmpty();
        RuleFor(request => request.OfficeNumber).NotEmpty().Unless(value => value is null);
        RuleFor(request => request.RegistryPhoneNumber).NotEmpty().RegistryPhoneNumber();
    }
}