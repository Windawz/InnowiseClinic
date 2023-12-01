using InnowiseClinic.Services.Authorization.API.Debugging.Filters;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Services.Authorization.API.Debugging.Attributes;

public class DebugControllerAttribute : TypeFilterAttribute
{
    public DebugControllerAttribute() : base(typeof(DebugControllerFilter)) { }
}