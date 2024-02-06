using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;

namespace InnowiseClinic.Microservices.Profiles.Api.Validators;

public class EditDoctorTargetValidator : AbstractValidator<EditDoctorTarget>
{
    public EditDoctorTargetValidator()
    {
        RuleFor(target => target.FirstName).NotEmpty();
        RuleFor(target => target.LastName).NotEmpty();
        RuleFor(target => target.MiddleName).NotEmpty().Unless(request => request.MiddleName is null);
        RuleFor(target => target.CareerStartYear).GreaterThanOrEqualTo(0);
        RuleFor(target => target.Status).ValidDoctorStatus();
    }
}