namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public interface IContainer
{
    ContainerKind Kind { get; }

    Task<DocumentDownloadInfo?> GetDownloadInfoAsync(Guid documentId);
    Task<Guid> UploadAsync(DocumentUploadInfo uploadInfo);
}