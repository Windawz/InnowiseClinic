using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Offices.Api.Mapping;
using InnowiseClinic.Microservices.Offices.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Offices.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Shared.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Offices.Api.Controllers;

[ApiController]
[Route("offices")]
[Authorize(Roles = RoleName.Receptionist)]
public class OfficesController : ControllerBase
{
    private readonly IOfficeService _officeService;
    private readonly IPhotoUploaderService _photoUploaderService;

    public OfficesController(
        IOfficeService officeService,
        IPhotoUploaderService photoUploaderService)
    {
        _officeService = officeService;
        _photoUploaderService = photoUploaderService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType<GetOfficeResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var office = await _officeService.GetOfficeAsync(id);
        var response = ResponseMapping.ToGetOfficeResponse(office);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType<ICollection<GetOfficeResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ICollection<GetOfficeResponse>>(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPage(int count, Guid? start)
    {
        var offices = await _officeService.GetOfficePageAsync(count, start);
        
        if (offices.Count > 0)
        {
            return Ok(offices);
        }
        else
        {
            return NoContent();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateOfficeRequest request, [FromForm] IFormFile? photo)
    {
        Guid? photoId = photo is not null
            ? await _photoUploaderService.UploadAsync(photo)
            : null;

        var input = RequestMapping.ToOfficeCreationInput(request, photoId);
        Guid id = await _officeService.CreateOfficeAsync(input);

        return CreatedAtAction(nameof(Get), new { Id = id });
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Edit(Guid id, EditOfficeRequest request, [FromForm] IFormFile? photo)
    {
        Guid? photoId = photo is not null
            ? await _photoUploaderService.UploadAsync(photo)
            : null;

        var input = RequestMapping.ToOfficeEditInput(request, photoId);

        await _officeService.EditOfficeAsync(id, input);

        return NoContent();
    }
}