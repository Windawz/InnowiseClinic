namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public interface IContainer
{
    ContainerKind Kind { get; }

    Task<DocumentDownloadInfo?> GetDownloadInfo(Guid documentId);
    Task<Guid> Upload(DocumentUploadInfo uploadInfo);
}