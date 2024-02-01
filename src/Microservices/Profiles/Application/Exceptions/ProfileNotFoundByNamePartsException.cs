using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Application.Exceptions;

public class ProfileNotFoundByNameException(Name Name)
        : ProfileNotFoundException($"Failed to find profile with name {Name}");