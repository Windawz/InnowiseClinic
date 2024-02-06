using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;
using InnowiseClinic.Microservices.Shared.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Profiles.Api.Controllers;

[ApiController]
[Route("doctors")]
public class DoctorController : ControllerBase
{
    [HttpGet("{id}")]
    [Authorize(Roles = $"{RoleName.Patient},{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ActionResult<GetDoctorResponse>> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Authorize(Roles = $"{RoleName.Patient},{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ActionResult<GetDoctorPageResponse>> GetPage(int count, int? offset)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Authorize(Roles = $"{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public Task<ActionResult> Create(CreateDoctorRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = $"{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<ActionResult> Edit(Guid id, JsonPatchDocument<EditDoctorTarget> patchDocument)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ActionResult> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}