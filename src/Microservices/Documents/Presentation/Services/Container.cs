using Azure;
using Azure.Storage.Blobs;
using InnowiseClinic.Microservices.Documents.Presentation.Services.Exceptions;

namespace InnowiseClinic.Microservices.Documents.Presentation.Services;

public class Container : IContainer
{
    private readonly BlobServiceClient _serviceClient;
    private BlobContainerClient? _cachedContainerClient;

    public Container(BlobServiceClient serviceClient, ContainerKind kind)
    {
        _serviceClient = serviceClient;
        Kind = kind;
    }

    public ContainerKind Kind { get; }

    public async Task<DocumentDownloadInfo?> GetDownloadInfoAsync(Guid documentId)
    {
        DocumentDownloadInfo? downloadInfo = null;

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
                string extension = Path.GetExtension(item.Name);

                downloadInfo = new DocumentDownloadInfo(
                    async () => (await blobClient.DownloadStreamingAsync()).Value.Content,
                    new DocumentInfo(
                        extension: extension));
            }
        }

        return downloadInfo;
    }

    public async Task<Guid> UploadAsync(DocumentUploadInfo uploadInfo)
    {
        var documentId = Guid.NewGuid();

        await UploadWithIdAsync(documentId, uploadInfo);

        return documentId;
    }

    public async Task UploadWithIdAsync(Guid documentId, DocumentUploadInfo uploadInfo)
    {
        string? extension = uploadInfo.DocumentInfo.Extension;

        if (extension is null || !Kind.PermittedExtensions.Contains(extension))
        {
            throw new UnpermittedDocumentExtensionException(Kind.Name, uploadInfo.DocumentInfo.Extension);
        }

        var containerClient = await GetContainerClientAsync();
        string blobName = $"{documentId}{extension}";
        var blobClient = containerClient.GetBlobClient(blobName);

        // `overwrite` isn't optional and only accepts `true`.
        await using var blobOutputStream = await blobClient.OpenWriteAsync(overwrite: true);
        using var uploadInputStream = uploadInfo.OpenInputStream();

        await uploadInputStream.CopyToAsync(blobOutputStream);
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
        string containerName = Kind.Name;

        try
        {
            containerClient = await _serviceClient.CreateBlobContainerAsync(containerName);
        }
        catch (RequestFailedException)
        {
            containerClient = _serviceClient.GetBlobContainerClient(containerName);
            bool exists = await containerClient.ExistsAsync();

            if (!exists)
            {
                throw new ContainerObtainmentException(containerName);
            }
        }

        _cachedContainerClient = containerClient;

        return _cachedContainerClient;
    }
}