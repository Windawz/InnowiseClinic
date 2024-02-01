namespace InnowiseClinic.Microservices.Profiles.Application.Exceptions;

public class ProfileNotFoundByIdException(Guid Id)
    : ProfileNotFoundException($"Failed to find profile with id {Id}");