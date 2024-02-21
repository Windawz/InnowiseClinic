namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public class DocumentDownloadInfo
{
    private readonly Func<Task<Stream>> _outputStreamFactory;

    public DocumentDownloadInfo(Func<Task<Stream>> outputStreamFactory, DocumentInfo documentInfo)
    {
        _outputStreamFactory = outputStreamFactory;
        DocumentInfo = documentInfo;
    }

    public DocumentInfo DocumentInfo { get; }

    public async Task<Stream> OpenOutputStreamAsync()
    {
        return await _outputStreamFactory();
    }
}