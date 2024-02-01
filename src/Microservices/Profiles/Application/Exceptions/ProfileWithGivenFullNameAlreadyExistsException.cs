using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Application.Exceptions;

public class ProfileWithGivenNameAlreadyExistsException(Name Name)
    : Exception($"Profile with name {Name} already exists");