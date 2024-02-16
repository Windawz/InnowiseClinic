using Azure;
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

        // Container creation throws if container already exists.
        //
        // There's a possible race condition that can occur
        // if two instances of the microservice attempt to
        // create the container.
        //
        // To avoid the race condition, creation is attempted first.
        // On failure, container is assumed to exist,
        // so a client for it is obtained.
        // If existance check on the client returns false,
        // then the creation attempt must've failed for some
        // other reason, so it's considered an unrecoverable error.
        //
        // TODO: The catch-block is now the primary execution path.
        // Need to find a better way.

        BlobContainerClient containerClient;

        try
        {
            containerClient = await _serviceClient.CreateBlobContainerAsync(_containerName);
        }
        catch (RequestFailedException)
        {
            containerClient = _serviceClient.GetBlobContainerClient(_containerName);
            bool exists = await containerClient.ExistsAsync();

            if (!exists)
            {
                throw new ContainerObtainmentException(_containerName);
            }
        }

        _cachedContainerClient = containerClient;

        return _cachedContainerClient;
    }
}