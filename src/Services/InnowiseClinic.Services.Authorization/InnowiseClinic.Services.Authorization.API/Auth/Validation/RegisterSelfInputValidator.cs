using FluentValidation;
using InnowiseClinic.Services.Authorization.API.Auth.DataTransfer;

namespace InnowiseClinic.Services.Authorization.API.Auth.Validation;

public class RegisterSelfInputValidator : AbstractValidator<RegisterSelfInput>
{
    public RegisterSelfInputValidator()
    {
        RuleFor(input => input.EmailAddress).EmailAddress();
        RuleFor(input => input.PasswordText).PasswordText();
    }
}