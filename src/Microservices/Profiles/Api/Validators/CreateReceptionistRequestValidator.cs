using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;

namespace InnowiseClinic.Microservices.Profiles.Api.Validators;

public class CreateReceptionistRequestValidator : AbstractValidator<CreateReceptionistRequest>
{
    public CreateReceptionistRequestValidator()
    {
        RuleFor(request => request.FirstName).NotEmpty();
        RuleFor(request => request.LastName).NotEmpty();
        RuleFor(request => request.MiddleName).NotEmpty().Unless(request => request.MiddleName is null);
    }
}