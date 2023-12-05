using FluentValidation;
using InnowiseClinic.Services.Authorization.API.Auth.DataTransfer.Input;

namespace InnowiseClinic.Services.Authorization.API.Auth.Validators;

public class RegisterSelfInputValidator : AbstractValidator<RegisterSelfInput>
{
    public RegisterSelfInputValidator()
    {
        RuleFor(input => input.EmailAddress).EmailAddress();
        RuleFor(input => input.PasswordText).PasswordText();
    }
}