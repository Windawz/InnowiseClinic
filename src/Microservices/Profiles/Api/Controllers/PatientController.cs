using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.Attributes;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;
using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Repositories.Filters;
using InnowiseClinic.Microservices.Profiles.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Shared.Api.Constants;
using InnowiseClinic.Microservices.Shared.Api.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Profiles.Api.Controllers;

[ApiController]
[Route("patients")]
[Authorize(Roles = $"{RoleName.Patient},{RoleName.Doctor},{RoleName.Receptionist}")]
public class PatientController : ControllerBase
{
    private readonly IPatientProfileService _patientProfileService;
    private readonly IValidator<EditPatientTarget> _editPatientTargetValidator;

    public PatientController(IPatientProfileService patientProfileService, IValidator<EditPatientTarget> editPatientTargetValidator)
    {
        _patientProfileService = patientProfileService;
        _editPatientTargetValidator = editPatientTargetValidator;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPatientResponse>> Get(Guid id)
    {
        var profile = await _patientProfileService.GetAsync(id);
        var response = ApiToApplicationMap.ToResponse(profile);

        return response;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<GetPatientPageResponse>>> GetPage(int count, int? offset)
    {
        var profiles = await _patientProfileService.GetManyAsync(lastPosition: offset, maxCount: count);
        var pageResponses = profiles.Select(ApiToApplicationMap.ToPageResponse).ToArray();

        return pageResponses;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [QueryParameterConstraint(nameof(firstName))]
    [QueryParameterConstraint(nameof(lastName))]
    public async Task<ActionResult<ICollection<GetPatientPageResponse>>> GetPageByName(
        int count,
        int? offset,
        string firstName,
        string lastName,
        string? middleName)
    {
        Name name = ApiToApplicationMap.ToName(firstName, lastName, middleName);
        var profiles = await _patientProfileService.GetManyByNameAsync(name: name, lastPosition: offset, maxCount: count);
        var pageResponses = profiles.Select(ApiToApplicationMap.ToPageResponse).ToArray();

        return pageResponses;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreatePatientRequest>> Create(CreatePatientRequest request)
    {
        var profile = ApiToApplicationMap.FromRequest(request);
        
        await _patientProfileService.CreateAsync(profile);

        return CreatedAtAction(nameof(Get), new { id = profile.Id!.Value }, request);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Edit(Guid id, JsonPatchDocument<EditPatientTarget> patchDocument)
    {
        var profile = await _patientProfileService.GetAsync(id);
        var target = ApiToApplicationMap.ToTarget(profile);

        await patchDocument.ApplyToAndValidateAsync(target, _editPatientTargetValidator, ModelState);
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _patientProfileService.EditAsync(id, editedProfile =>
            ApiToApplicationMap.ApplyTarget(editedProfile, target));

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _patientProfileService.DeleteAsync(id);

        return Ok();
    }
}