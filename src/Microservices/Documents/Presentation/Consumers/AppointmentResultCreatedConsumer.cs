using InnowiseClinic.Microservices.Documents.Presentation.Services;
using InnowiseClinic.Microservices.Shared.Api.Contracts;
using MassTransit;

namespace InnowiseClinic.Microservices.Documents.Presentation.Consumers;

public class AppointmentResultCreatedConsumer : IConsumer<AppointmentResultCreated>
{
    private readonly IContainer _container;

    public AppointmentResultCreatedConsumer(IContainerProvider containerProvider)
    {
        _container = containerProvider.GetOfKind(ContainerKind.AppointmentResults);
    }

    public async Task Consume(ConsumeContext<AppointmentResultCreated> context)
    {
        Guid documentId = context.Message.AppointmentId;
        string? extension = context.Message.Extension;
        byte[] appointmentResultData = context.Message.AppointmentResultData;

        var uploadInfo = new DocumentUploadInfo(
            () => new MemoryStream(appointmentResultData),
            new DocumentInfo(extension));

        await _container.UploadWithIdAsync(documentId, uploadInfo);
    }
}