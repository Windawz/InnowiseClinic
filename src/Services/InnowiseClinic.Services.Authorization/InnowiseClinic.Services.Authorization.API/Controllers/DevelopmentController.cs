using InnowiseClinic.Services.Authorization.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Services.Authorization.API.Controllers;

[ApiController]
[TypeFilter(typeof(DevelopmentControllerFilterAttribute), IsReusable = true)]
public abstract class DevelopmentController : ControllerBase { }