using FluentValidation;
using FluentValidation.Validators;
using InnowiseClinic.Services.Authorization.API.DataTransfer;

namespace InnowiseClinic.Services.Authorization.API.Services.Validation.Input;

public class RegisterSelfInputValidator : AbstractValidator<RegisterSelfInput>
{
    public RegisterSelfInputValidator()
    {
        RuleFor(input => input.EmailAddress).EmailAddress();
        RuleFor(input => input.PasswordText).PasswordText();
    }
}