using Azure.Storage.Blobs;
using InnowiseClinic.Microservices.Documents.Presentation.Services.Exceptions;

namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public class DocumentContainer : IDocumentContainer
{
    private static readonly string _containerName = "documents";
    private readonly BlobServiceClient _serviceClient;
    private BlobContainerClient? _cachedContainerClient;

    public DocumentContainer(BlobServiceClient serviceClient)
    {
        _serviceClient = serviceClient;
    }

    public async Task<Stream?> OpenDownloadStreamIfExistsAsync(Guid documentId)
    {
        Stream? downloadStream = null;

        var client = await GetContainerClientAsync();
        var pages = client.GetBlobsAsync(prefix: documentId.ToString())
            .AsPages();

        await using var enumerator = pages.GetAsyncEnumerator();

        if (await enumerator.MoveNextAsync())
        {
            var page = enumerator.Current;
            var item = page.Values.SingleOrDefault();
            
            if (item is not null)
            {
                var blobClient = client.GetBlobClient(item.Name);
                var response = await blobClient.DownloadStreamingAsync();
      
                downloadStream = response.Value.Content;
            }
        }

        return downloadStream;
    }

    public async Task<Guid> UploadFromStreamAsync(Stream inputStream)
    {
        var documentId = Guid.NewGuid();
        var containerClient = await GetContainerClientAsync();
        var blobClient = containerClient.GetBlobClient(blobName: documentId.ToString());
        
        // `overwrite` isn't optional and only accepts `true`.
        await using var blobWriteStream = await blobClient.OpenWriteAsync(overwrite: true);

        await inputStream.CopyToAsync(blobWriteStream);

        return documentId;
    }

    private async Task<BlobContainerClient> GetContainerClientAsync()
    {
        if (_cachedContainerClient is not null)
        {
            return _cachedContainerClient;
        }

        _cachedContainerClient = _serviceClient.GetBlobContainerClient(_containerName);

        if (!await _cachedContainerClient.ExistsAsync())
        {
            var response = await _serviceClient.CreateBlobContainerAsync(_containerName);

            try
            {
                _cachedContainerClient = response.Value;
            }
            catch (Exception inner)
            {
                throw new ContainerCreationFailedException(_containerName, inner);
            }
        }

        return _cachedContainerClient;
    }
}