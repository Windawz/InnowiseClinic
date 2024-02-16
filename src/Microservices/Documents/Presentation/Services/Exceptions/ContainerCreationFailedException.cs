namespace InnowiseClinic.Microservices.Documents.Presentation.Services.Exceptions;

public class ContainerCreationFailedException(string containerName, Exception? innerException)
    : Exception(
        innerException: innerException,
        message: $"Failed to create container '{containerName}'");