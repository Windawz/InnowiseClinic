namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public interface IOutputContentTypeMapperProvider
{
    IOutputContentTypeMapper GetForContainerKind(ContainerKind kind);
}