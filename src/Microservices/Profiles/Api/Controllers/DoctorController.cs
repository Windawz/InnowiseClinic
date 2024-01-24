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
    [ProducesResponseType<GetPatientResponse>(StatusCodes.Status200OK)]
    public IActionResult Get(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Authorize(Roles = $"{RoleName.Patient},{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType<ICollection<GetPatientPageResponse>>(StatusCodes.Status200OK)]
    public IActionResult GetPage(int count, int? offset)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Authorize(Roles = $"{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult Create(CreateDoctorRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = $"{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Edit(Guid id, JsonPatchDocument<EditPatientTarget> patchDocument)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{RoleName.Doctor},{RoleName.Receptionist}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}