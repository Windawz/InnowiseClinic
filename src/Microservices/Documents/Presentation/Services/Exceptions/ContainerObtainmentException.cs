namespace InnowiseClinic.Microservices.Documents.Presentation.Services.Exceptions;

public class ContainerObtainmentException(string containerName)
    : Exception($"Failed to obtain container '{containerName}'");