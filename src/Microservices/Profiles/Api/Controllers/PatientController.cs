using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;
using InnowiseClinic.Microservices.Shared.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Profiles.Api.Controllers;

[ApiController]
[Authorize(Roles = $"{RoleName.Patient},{RoleName.Doctor},{RoleName.Receptionist}")]
public class PatientController : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType<GetPatientResponse>(StatusCodes.Status200OK)]
    public IActionResult Get(Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult Create(CreatePatientRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Edit(Guid id, JsonPatchDocument<EditPatientTarget> patchDocument)
    {
        throw new NotImplementedException();
    }
}