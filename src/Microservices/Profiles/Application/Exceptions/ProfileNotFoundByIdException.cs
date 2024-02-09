namespace InnowiseClinic.Microservices.Profiles.Application.Exceptions;

public class ProfileNotFoundByIdException(Guid Id)
    : Exception($"Failed to find profile with id {Id}");