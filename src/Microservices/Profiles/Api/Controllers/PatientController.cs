using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Shared.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Microservices.Profiles.Api.Controllers;

[ApiController]
[Authorize(Roles = RoleName.Patient + "," + RoleName.Doctor + "," + RoleName.Receptionist)]
public class PatientController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreatePatientRequest request)
    {
        throw new NotImplementedException();
    }
}