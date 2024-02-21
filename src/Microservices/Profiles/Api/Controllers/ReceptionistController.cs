using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.Attributes;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;
using InnowiseClinic.Microservices.Profiles.Api.Services;
using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Repositories.Filtering;
using InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Shared.Api.Constants;
using InnowiseClinic.Microservices.Shared.Api.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Profiles.Api.Controllers;

[ApiController]
[Route("receptionists")]
[Authorize(Roles = RoleName.Receptionist)]
public class ReceptionistController : ControllerBase
{
    private readonly IReceptionistProfileService _receptionistProfileService;

    public ReceptionistController(IReceptionistProfileService receptionistProfileService)
    {
        _receptionistProfileService = receptionistProfileService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetReceptionistResponse>> Get(Guid id)
    {
        var profile = await _receptionistProfileService.GetAsync(id);
        var response = ApiToApplicationMap.ToResponse(profile);

        return response;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<GetReceptionistPageResponse>>> GetPage(int? lastPosition, int? maxCount)
    {
        var profiles = await _receptionistProfileService.GetManyAsync(lastPosition: lastPosition, maxCount: maxCount);
        var pageResponses = profiles.Select(ApiToApplicationMap.ToPageResponse).ToArray();

        return pageResponses;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [QueryParameterConstraint(nameof(firstName), nameof(lastName), nameof(middleName))]
    public async Task<ActionResult<ICollection<GetReceptionistPageResponse>>> GetPageByName(
        int? lastPosition,
        int? maxCount,
        string? firstName,
        string? lastName,
        string? middleName)
    {
        var filteredName = new FilteredName(firstName, lastName, middleName);
        var profiles = await _receptionistProfileService.GetManyByNameAsync(
            filteredName: filteredName,
            lastPosition: lastPosition,
            maxCount: maxCount);
        var pageResponses = profiles.Select(ApiToApplicationMap.ToPageResponse).ToArray();

        return pageResponses;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreateReceptionistRequest request)
    {
        var profile = ApiToApplicationMap.FromRequest(request);
        
        await _receptionistProfileService.CreateAsync(profile);

        return CreatedAtAction(nameof(Get), new { id = profile.Id!.Value }, request);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Edit(
        Guid id,
        JsonPatchDocument<EditReceptionistTarget> patchDocument,
        [FromServices] IValidator<EditReceptionistTarget> validator)
    {
        var profile = await _receptionistProfileService.GetAsync(id);
        var target = ApiToApplicationMap.ToTarget(profile);

        await patchDocument.ApplyToAndValidateAsync(target, validator, ModelState);
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _receptionistProfileService.EditAsync(id, editedProfile =>
            ApiToApplicationMap.ApplyTarget(editedProfile, target));

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _receptionistProfileService.DeleteAsync(id);

        return Ok();
    }

    [HttpPut("{id}/photo")]
    [Authorize(Roles = RoleName.Receptionist)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePhoto(
        Guid id,
        IFormFile photoFile,
        [FromServices] IPhotoService photoService)
    {
        string? extension = Path.GetExtension(photoFile.FileName);
        await using var stream = photoFile.OpenReadStream();

        await photoService.UpdateAsync<ReceptionistProfile>(id, stream, extension);

        return Ok();
    }
}