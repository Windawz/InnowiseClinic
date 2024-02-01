namespace InnowiseClinic.Microservices.Profiles.Application.Exceptions;

public class ProfileNotFoundException(string message) : Exception(message);