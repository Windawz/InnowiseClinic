using FluentValidation;
using InnowiseClinic.Services.Authorization.API.DataTransfer.Input;

namespace InnowiseClinic.Services.Authorization.API.Validators;

public class RegisterSelfInputValidator : AbstractValidator<RegisterSelfInput>
{
    public RegisterSelfInputValidator()
    {
        RuleFor(input => input.EmailAddress).EmailAddress();
        RuleFor(input => input.PasswordText).PasswordText();
    }
}