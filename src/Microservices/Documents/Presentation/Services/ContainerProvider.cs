using Azure.Storage.Blobs;

namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public class ContainerProvider : IContainerProvider
{
    private readonly BlobServiceClient _serviceClient;

    public ContainerProvider(BlobServiceClient serviceClient)
    {
        _serviceClient = serviceClient;
    }

    public IContainer GetOfKind(ContainerKind kind)
    {
        return new Container(_serviceClient, kind);
    }
}