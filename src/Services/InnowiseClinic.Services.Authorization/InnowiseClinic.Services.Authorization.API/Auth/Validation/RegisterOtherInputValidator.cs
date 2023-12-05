using FluentValidation;
using InnowiseClinic.Services.Authorization.API.Auth.DataTransfer.Input;

namespace InnowiseClinic.Services.Authorization.API.Auth.Validators;

public class RegisterOtherInputValidator : AbstractValidator<RegisterOtherInput>
{
    public RegisterOtherInputValidator()
    {
        RuleFor(input => input.EmailAddress).EmailAddress();
        RuleFor(input => input.PasswordText).PasswordText();
        RuleFor(input => input.RoleName).RoleName();
    }
}