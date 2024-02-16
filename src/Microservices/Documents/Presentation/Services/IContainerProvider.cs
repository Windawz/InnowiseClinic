namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public interface IContainerProvider
{
    IContainer GetOfKind(ContainerKind kind);
}