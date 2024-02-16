using InnowiseClinic.Microservices.Documents.Presentation.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Documents.Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentContainer _container;

    public DocumentsController(IDocumentContainer container)
    {
        _container = container;
    }

    [HttpGet("{id}")]
    [Produces("application/octet-stream")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetById(Guid id)
    {
        // FileStreamResult disposes of the passed stream.
        // Hence no `await using`.
        // See `https://stackoverflow.com/questions/26275764/does-filestreamresult-close-stream`.
        var downloadStream = await _container.OpenDownloadStreamIfExistsAsync(id);

        return downloadStream is not null
            ? new FileStreamResult(downloadStream, "application/octet-stream")
            : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Post(IFormFile documentFile)
    {
        await using var documentStream = documentFile.OpenReadStream();
        Guid documentId = await _container.UploadFromStreamAsync(documentStream);

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = documentId },
            value: null);
    }
}