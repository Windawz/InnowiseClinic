using FluentValidation;
using FluentValidation.Validators;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;

namespace InnowiseClinic.Microservices.Authorization.Api.Validators;

public class LogInRequestValidator : AbstractValidator<LogInRequest>
{
    public LogInRequestValidator()
    {
        RuleFor(request => request.Email).EmailAddress();
        RuleFor(request => request.Password).Password();
    }
}