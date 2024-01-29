using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;
using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects;

public static class Map
{
    public static PatientProfile FromRequest(CreatePatientRequest value)
    {
        return new(
            Id: default,
            AccountId: value.AccountId,
            FirstName: value.FirstName.Trim(),
            LastName: value.LastName.Trim(),
            MiddleName: value.MiddleName?.Trim(),
            PhoneNumber: value.PhoneNumber.Trim(),
            DateOfBirth: value.DateOfBirth);
    }

    public static DoctorProfile FromRequest(CreateDoctorRequest value)
    {
        return new(
            Id: default,
            AccountId: value.AccountId,
            OfficeId: value.OfficeId,
            SpecializationId: value.SpecializationId,
            FirstName: value.FirstName.Trim(),
            LastName: value.LastName.Trim(),
            MiddleName: value.MiddleName?.Trim(),
            DateOfBirth: value.DateOfBirth,
            CareerStartYear: value.CareerStartYear,
            // Validation is supposed to happen elsewhere.
            Status: (DoctorStatus)value.Status);
    }

    public static ReceptionistProfile FromRequest(CreateReceptionistRequest value)
    {
        return new(
            Id: default,
            AccountId: value.AccountId,
            OfficeId: value.OfficeId,
            FirstName: value.FirstName.Trim(),
            LastName: value.LastName.Trim(),
            MiddleName: value.MiddleName?.Trim());
    }

    public static GetPatientResponse ToResponse(PatientProfile value)
    {
        return new()
        {
            AccountId = value.AccountId,
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
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
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
            DateOfBirth = value.DateOfBirth,
            CareerStartYear = value.CareerStartYear,
            Experience = value.Experience,
            Status = (int)value.Status,
        };
    }

    public static GetReceptionistResponse ToResponse(ReceptionistProfile value)
    {
        return new()
        {
            AccountId = value.AccountId,
            OfficeId = value.OfficeId,
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
        };
    }

    public static GetPatientPageResponse ToPageResponse(PatientProfile value)
    {
        return new()
        {
            Id = value.Id,
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
            PhoneNumber = value.PhoneNumber,
        };
    }

    public static GetDoctorPageResponse ToPageResponse(DoctorProfile value)
    {
        return new()
        {
            Id = value.Id,
            OfficeId = value.OfficeId,
            SpecializationId = value.SpecializationId,
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
            Experience = value.Experience,
        };
    }

    public static GetReceptionistPageResponse ToPageResponse(ReceptionistProfile value)
    {
        return new()
        {
            Id = value.Id,
            OfficeId = value.OfficeId,
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
        };
    }

    public static EditPatientTarget ToTarget(PatientProfile value)
    {
        return new()
        {
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
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
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
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
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
        };
    }

    public static PatientProfile FromTarget(EditPatientTarget value, PatientProfile original)
    {
        return new(
            Id: original.Id,
            AccountId: original.AccountId,
            FirstName: value.FirstName,
            LastName: value.LastName,
            MiddleName: value.MiddleName,
            PhoneNumber: value.PhoneNumber,
            DateOfBirth: value.DateOfBirth);
    }

    public static DoctorProfile FromTarget(EditDoctorTarget value, DoctorProfile original)
    {
        return new(
            Id: original.Id,
            AccountId: original.AccountId,
            OfficeId: value.OfficeId,
            SpecializationId: value.SpecializationId,
            FirstName: value.FirstName,
            LastName: value.LastName,
            MiddleName: value.MiddleName,
            DateOfBirth: value.DateOfBirth,
            CareerStartYear: value.CareerStartYear,
            // Validation is supposed to happen elsewhere.
            Status: (DoctorStatus)value.Status);
    }

    public static ReceptionistProfile FromTarget(EditReceptionistTarget value, ReceptionistProfile original)
    {
        return new(
            Id: original.Id,
            AccountId: original.AccountId,
            OfficeId: value.OfficeId,
            FirstName: value.FirstName,
            LastName: value.LastName,
            MiddleName: value.MiddleName);
    }
}