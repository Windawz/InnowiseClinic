using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;
using InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Shared.Api.Constants;
using InnowiseClinic.Microservices.Shared.Api.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Profiles.Api.Controllers;

[ApiController]
[Route("doctors")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorProfileService _doctorProfileService;
    private readonly IValidator<EditDoctorTarget> _editDoctorTargetValidator;

    public DoctorController(IDoctorProfileService doctorProfileService, IValidator<EditDoctorTarget> editDoctorTargetValidator)
    {
        _doctorProfileService = doctorProfileService;
        _editDoctorTargetValidator = editDoctorTargetValidator;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{RoleName.Patient},{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetDoctorResponse>> Get(Guid id)
    {
        var profile = await _doctorProfileService.GetAsync(id);
        var response = ApiToApplicationMap.ToResponse(profile);

        return response;
    }

    [HttpGet]
    [Authorize(Roles = $"{RoleName.Patient},{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<GetDoctorPageResponse>>> GetPage(int count, int? offset)
    {
        var profiles = await _doctorProfileService.GetManyAsync(lastPosition: offset, maxCount: count);
        var pageResponses = profiles.Select(ApiToApplicationMap.ToPageResponse).ToArray();

        return pageResponses;
    }
    
    [HttpPost]
    [Authorize(Roles = $"{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreateDoctorRequest request)
    {
        var profile = ApiToApplicationMap.FromRequest(request);
        
        await _doctorProfileService.CreateAsync(profile);

        return CreatedAtAction(nameof(Get), new { id = profile.Id!.Value }, request);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = $"{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Edit(Guid id, JsonPatchDocument<EditDoctorTarget> patchDocument)
    {
        var profile = await _doctorProfileService.GetAsync(id);
        var target = ApiToApplicationMap.ToTarget(profile);

        await patchDocument.ApplyToAndValidateAsync(target, _editDoctorTargetValidator, ModelState);
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _doctorProfileService.EditAsync(id, editedProfile =>
            ApiToApplicationMap.ApplyTarget(editedProfile, target));

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _doctorProfileService.DeleteAsync(id);

        return Ok();
    }
}