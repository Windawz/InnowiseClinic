namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public interface IDocumentContainer
{
    Task<Stream?> OpenDownloadStreamIfExistsAsync(Guid documentId);
    Task<Guid> UploadFromStreamAsync(Stream inputStream);
}