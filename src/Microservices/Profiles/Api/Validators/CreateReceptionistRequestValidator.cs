using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;

namespace InnowiseClinic.Microservices.Profiles.Api.Validators;

public class CreateReceptionistRequestValidator : AbstractValidator<CreateReceptionistRequest>
{
    public CreateReceptionistRequestValidator()
    {
        RuleFor(request => request.FirstName).NamePart();
        RuleFor(request => request.LastName).NamePart();
        RuleFor(request => request.MiddleName).OptionalNamePart();
    }
}