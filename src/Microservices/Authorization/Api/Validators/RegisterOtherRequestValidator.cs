using FluentValidation;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Validators;

public class RegisterOtherRequestValidator : AbstractValidator<RegisterOtherRequest>
{
    public RegisterOtherRequestValidator()
    {
        RuleFor(request => request.Email).EmailAddress();
        RuleFor(request => request.Password).Password();
        RuleFor(request => request.ConfirmationPassword).Password().Equal(request => request.Password);
        RuleFor(request => request.Role).IsEnumName(typeof(Role), caseSensitive: false);
    }
}