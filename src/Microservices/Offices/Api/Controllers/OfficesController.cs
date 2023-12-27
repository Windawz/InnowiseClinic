using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Offices.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Shared.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Offices.Api.Controllers;

[ApiController]
[Route("offices")]
[Authorize(Roles = RoleName.Receptionist)]
public class OfficesController : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType<GetOfficeResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [ProducesResponseType<ICollection<GetOfficeResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ICollection<GetOfficeResponse>>(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetPage(int count, Guid? start)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public Task<IActionResult> Create(CreateOfficeRequest request, [FromForm] IFormFile? photo)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> Edit(Guid id, EditOfficeRequest request, [FromForm] IFormFile? photo)
    {
        throw new NotImplementedException();
    }
}