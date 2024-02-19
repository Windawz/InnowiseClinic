using InnowiseClinic.Microservices.Documents.Presentation.Contracts;
using InnowiseClinic.Microservices.Documents.Presentation.Services;
using MassTransit;

namespace InnowiseClinic.Microservices.Documents.Presentation.Consumers;

public class ProfilePhotoUpdatedConsumer : IConsumer<ProfilePhotoUpdated>
{
    private readonly IContainer _container;

    public ProfilePhotoUpdatedConsumer(IContainerProvider containerProvider)
    {
        _container = containerProvider.GetOfKind(ContainerKind.Photos);
    }

    public async Task Consume(ConsumeContext<ProfilePhotoUpdated> context)
    {
        Guid accountId = context.Message.AccountId;
        string? extension = context.Message.Extension;
        byte[] photoData = context.Message.PhotoData;

        var uploadInfo = new DocumentUploadInfo(
            () => new MemoryStream(photoData),
            new DocumentInfo(extension));

        await _container.UploadWithIdAsync(accountId, uploadInfo);
    }
}