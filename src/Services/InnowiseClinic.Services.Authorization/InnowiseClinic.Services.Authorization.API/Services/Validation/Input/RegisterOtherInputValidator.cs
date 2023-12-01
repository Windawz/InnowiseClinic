using FluentValidation;
using InnowiseClinic.Services.Authorization.API.DataTransfer;

namespace InnowiseClinic.Services.Authorization.API.Services.Validation.Input;

public class RegisterOtherInputValidator : AbstractValidator<RegisterOtherInput>
{
    public RegisterOtherInputValidator()
    {
        RuleFor(input => input.EmailAddress).EmailAddress();
        RuleFor(input => input.PasswordText).PasswordText();
        RuleFor(input => input.RoleName).RoleName();
    }
}