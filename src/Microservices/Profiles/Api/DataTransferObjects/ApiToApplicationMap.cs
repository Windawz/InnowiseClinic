using System.Globalization;
using System.Runtime.CompilerServices;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;
using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects;

public static class ApiToApplicationMap
{
    public static PatientProfile FromRequest(CreatePatientRequest value)
    {
        return new PatientProfile(
            accountId: value.AccountId,
            name: ToName(value.FirstName, value.LastName, value.MiddleName),
            phoneNumber: value.PhoneNumber.Trim(),
            dateOfBirth: value.DateOfBirth);
    }

    public static DoctorProfile FromRequest(CreateDoctorRequest value)
    {
        return new DoctorProfile(
            accountId: value.AccountId,
            officeId: value.OfficeId,
            specializationId: value.SpecializationId,
            name: ToName(value.FirstName, value.LastName, value.MiddleName),
            dateOfBirth: value.DateOfBirth,
            careerStartYear: value.CareerStartYear,
            status: (DoctorStatus)value.Status);
    }

    public static ReceptionistProfile FromRequest(CreateReceptionistRequest value)
    {
        return new ReceptionistProfile(
            accountId: value.AccountId,
            officeId: value.OfficeId,
            name: ToName(value.FirstName, value.LastName, value.MiddleName));
    }

    public static GetPatientResponse ToResponse(PatientProfile value)
    {
        return new()
        {
            AccountId = value.AccountId,
            FirstName = value.Name.First,
            LastName = value.Name.Last,
            MiddleName = value.Name.Middle,
            PhoneNumber = value.PhoneNumber,
            DateOfBirth = value.DateOfBirth,
        };
    }

    public static GetDoctorResponse ToResponse(DoctorProfile value)
    {
        return new()
        {
            AccountId = value.AccountId,
            OfficeId = value.OfficeId,
            SpecializationId = value.SpecializationId,
            FirstName = value.Name.First,
            LastName = value.Name.Last,
            MiddleName = value.Name.Middle,
            DateOfBirth = value.DateOfBirth,
            CareerStartYear = value.CareerStartYear,
            Experience = value.ExperienceYearCount,
            Status = (int)value.Status,
        };
    }

    public static GetReceptionistResponse ToResponse(ReceptionistProfile value)
    {
        return new()
        {
            AccountId = value.AccountId,
            OfficeId = value.OfficeId,
            FirstName = value.Name.First,
            LastName = value.Name.Last,
            MiddleName = value.Name.Middle,
        };
    }

    public static GetPatientPageResponse ToPageResponse(PatientProfile value)
    {
        return new()
        {
            Id = GetIdForResponse(value),
            FirstName = value.Name.First,
            LastName = value.Name.Last,
            MiddleName = value.Name.Middle,
            PhoneNumber = value.PhoneNumber,
        };
    }

    public static GetDoctorPageResponse ToPageResponse(DoctorProfile value)
    {
        return new()
        {
            Id = GetIdForResponse(value),
            OfficeId = value.OfficeId,
            SpecializationId = value.SpecializationId,
            FirstName = value.Name.First,
            LastName = value.Name.Last,
            MiddleName = value.Name.Middle,
            Experience = value.ExperienceYearCount,
        };
    }

    public static GetReceptionistPageResponse ToPageResponse(ReceptionistProfile value)
    {
        return new()
        {
            Id = GetIdForResponse(value),
            OfficeId = value.OfficeId,
            FirstName = value.Name.First,
            LastName = value.Name.Last,
            MiddleName = value.Name.Middle,
        };
    }

    public static EditPatientTarget ToTarget(PatientProfile value)
    {
        return new()
        {
            FirstName = value.Name.First,
            LastName = value.Name.Last,
            MiddleName = value.Name.Middle,
            PhoneNumber = value.PhoneNumber,
            DateOfBirth = value.DateOfBirth,
        };
    }

    public static EditDoctorTarget ToTarget(DoctorProfile value)
    {
        return new()
        {
            OfficeId = value.OfficeId,
            SpecializationId = value.SpecializationId,
            FirstName = value.Name.First,
            LastName = value.Name.Last,
            MiddleName = value.Name.Middle,
            DateOfBirth = value.DateOfBirth,
            CareerStartYear = value.CareerStartYear,
            Status = (int)value.Status,
        };
    }

    public static EditReceptionistTarget ToTarget(ReceptionistProfile value)
    {
        return new()
        {
            OfficeId = value.OfficeId,
            FirstName = value.Name.First,
            LastName = value.Name.Last,
            MiddleName = value.Name.Middle,
        };
    }

    public static void ApplyTarget(PatientProfile value, EditPatientTarget target)
    {
        value.Name = ToName(target.FirstName, target.LastName, target.MiddleName);
        value.PhoneNumber = target.PhoneNumber.Trim();
        value.DateOfBirth = target.DateOfBirth;
    }

    public static void ApplyTarget(DoctorProfile value, EditDoctorTarget target)
    {
        value.OfficeId = target.OfficeId;
        value.SpecializationId = target.SpecializationId;
        value.Name = ToName(target.FirstName, target.LastName, target.MiddleName);
        value.DateOfBirth = target.DateOfBirth;
        value.CareerStartYear = target.CareerStartYear;
        value.Status = (DoctorStatus)target.Status;
    }

    public static void ApplyTarget(ReceptionistProfile value, EditReceptionistTarget target)
    {
        value.OfficeId = target.OfficeId;
        value.Name = ToName(target.FirstName, target.LastName, target.MiddleName);
    }

    private static Name ToName(string firstName, string lastName, string? middleName)
    {
        static string CleanUp(string namePart)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(namePart.Trim());
        }

        return new(CleanUp(firstName), CleanUp(lastName), middleName is not null ? CleanUp(middleName) : null);
    }

    private static Guid GetIdForResponse<TEntity>(
        TEntity entity,
        [CallerArgumentExpression(nameof(entity))]
        string? paramName = null) where TEntity : Entity
    {
        return entity.Id ?? throw new ArgumentException(
                paramName: paramName ?? nameof(entity),
                message: $"Cannot map transient entity {entity} to response");
    }
}