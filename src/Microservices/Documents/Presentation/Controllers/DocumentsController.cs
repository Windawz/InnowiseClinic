using InnowiseClinic.Microservices.Documents.Presentation.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Documents.Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
    private readonly IContainer _container;

    public DocumentsController(IContainerProvider containerProvider)
    {
        _container = containerProvider.GetOfKind(ContainerKind.Photos);
    }

    [HttpGet("{id}")]
    [Produces("application/octet-stream")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetById(Guid id)
    {
        var downloadInfo = await _container.GetDownloadInfo(id);

        // FileStreamResult disposes of the passed stream.
        // Hence no `await using`.
        // See `https://stackoverflow.com/questions/26275764/does-filestreamresult-close-stream`.
        return downloadInfo is null
            ? NotFound()
            : new FileStreamResult(
                await downloadInfo.OpenOutputStreamAsync(),
                "application/octet-stream");
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Post(IFormFile documentFile)
    {
        string? extension = Path.GetExtension(documentFile.FileName);

        Guid documentId = await _container.Upload(new DocumentUploadInfo(
            documentFile.OpenReadStream,
            new DocumentInfo(
                extension: extension)));

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = documentId },
            value: null);
    }
}