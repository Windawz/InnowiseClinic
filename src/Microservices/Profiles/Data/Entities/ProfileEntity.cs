namespace InnowiseClinic.Microservices.Profiles.Data.Entities;

public abstract record ProfileEntity(
    Guid Id,
    Guid AccountId,
    string FirstName,
    string LastName,
    string? MiddleName);