using System.Globalization;
using InnowiseClinic.Microservices.Profiles.Data.Entities;

namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public static class ApplicationToDataMap
{
    public static PatientProfile ToModel(PatientEntity value)
    {
        return new(
            Id: value.Id,
            AccountId: value.AccountId,
            Name: new(value.FirstName, value.LastName, value.MiddleName),
            PhoneNumber: value.PhoneNumber,
            DateOfBirth: value.DateOfBirth);
    }

    public static DoctorProfile ToModel(DoctorEntity value)
    {
        return new(
            Id: value.Id,
            AccountId: value.AccountId,
            OfficeId: value.OfficeId,
            SpecializationId: value.SpecializationId,
            Name: new(value.FirstName, value.LastName, value.MiddleName),
            DateOfBirth: value.DateOfBirth,
            CareerStartYear: value.CareerStartYear,
            Status: (DoctorStatus)value.Status);
    }

    public static ReceptionistProfile ToModel(ReceptionistEntity value)
    {
        return new(
            Id: value.Id,
            AccountId: value.AccountId,
            OfficeId: value.OfficeId,
            Name: new(value.FirstName, value.LastName, value.MiddleName));
    }

    public static PatientEntity ToEntity(PatientProfile value)
    {
        var (firstName, lastName, middleName) = ToEntityNameParts(value.Name);

        return new(
            Id: value.Id,
            AccountId: value.AccountId,
            FirstName: firstName,
            LastName: lastName,
            MiddleName: middleName,
            PhoneNumber: value.PhoneNumber,
            DateOfBirth: value.DateOfBirth);
    }

    public static DoctorEntity ToEntity(DoctorProfile value)
    {
        var (firstName, lastName, middleName) = ToEntityNameParts(value.Name);

        return new(
            Id: value.Id,
            AccountId: value.AccountId,
            OfficeId: value.OfficeId,
            SpecializationId: value.SpecializationId,
            FirstName: firstName,
            LastName: lastName,
            MiddleName: middleName,
            DateOfBirth: value.DateOfBirth,
            CareerStartYear: value.CareerStartYear,
            Status: (int)value.Status);
    }
    
    public static ReceptionistEntity ToEntity(ReceptionistProfile value)
    {
        var (firstName, lastName, middleName) = ToEntityNameParts(value.Name);

        return new(
            Id: value.Id,
            AccountId: value.AccountId,
            OfficeId: value.OfficeId,
            FirstName: firstName,
            LastName: lastName,
            MiddleName: middleName);
    }

    private static (string First, string Last, string? Middle) ToEntityNameParts(Name name)
    {
        static string ToLower(string namePart)
        {
            // Names are culturally sensitive, but
            // the scope of the project doesn't really
            // include working with different cultures.
            //
            // It's still good to be explicit about what culture
            // is being used in this case, in my opinion.
            return CultureInfo.InvariantCulture.TextInfo.ToLower(namePart);
        }

        return (
            ToLower(name.First),
            ToLower(name.Last),
            name.Middle is not null
                ? ToLower(name.Middle)
                : null);
    }
}