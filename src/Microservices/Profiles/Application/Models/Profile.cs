namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public abstract record Profile(
    Guid Id,
    Guid AccountId,
    Name Name);