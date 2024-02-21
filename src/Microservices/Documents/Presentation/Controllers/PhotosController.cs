using InnowiseClinic.Microservices.Documents.Presentation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Documents.Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class PhotosController : ControllerBase
{
    private readonly IContainer _container;
    private readonly IOutputContentTypeMapper _contentTypeMapper;

    public PhotosController(
        IContainerProvider containerProvider,
        IOutputContentTypeMapperProvider contentTypeMapperProvider)
    {
        var containerKind = ContainerKind.Photos;

        _container = containerProvider.GetOfKind(containerKind);
        _contentTypeMapper = contentTypeMapperProvider.GetForContainerKind(containerKind);
    }

    [HttpGet("{id}")]
    [Produces("application/octet-stream")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult> GetById(Guid id)
    {
        DocumentDownloadInfo? downloadInfo = await _container.GetDownloadInfoAsync(id);

        if (downloadInfo is not null)
        {
            string? extension = downloadInfo.DocumentInfo.Extension;
            var contentType = extension is not null
                ? _contentTypeMapper.MapExtension(extension)
                : "application/octet-stream";

            // FileStreamResult disposes of the passed stream.
            return new FileStreamResult(
                await downloadInfo.OpenOutputStreamAsync(),
                contentType);
        }

        return NotFound();
    }
}