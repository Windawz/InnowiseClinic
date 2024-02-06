using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;
using InnowiseClinic.Microservices.Shared.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Profiles.Api.Controllers;

[ApiController]
[Route("receptionists")]
[Authorize(Roles = RoleName.Receptionist)]
public class ReceptionistController : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ActionResult<GetReceptionistResponse>> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ActionResult<ICollection<GetReceptionistPageResponse>>> GetPage(int count, int? offset)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public Task<ActionResult> Create(CreateReceptionistRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<ActionResult> Edit(Guid id, JsonPatchDocument<EditReceptionistTarget> patchDocument)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ActionResult> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}