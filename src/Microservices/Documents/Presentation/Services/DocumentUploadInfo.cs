namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public class DocumentUploadInfo
{
    private Func<Stream> _inputStreamFactory;

    public DocumentUploadInfo(Func<Stream> inputStreamFactory, DocumentInfo documentInfo)
    {
        _inputStreamFactory = inputStreamFactory;
        DocumentInfo = documentInfo;
    }
    
    public DocumentInfo DocumentInfo { get; }

    public Stream OpenInputStream()
    {
        return _inputStreamFactory();
    }
}