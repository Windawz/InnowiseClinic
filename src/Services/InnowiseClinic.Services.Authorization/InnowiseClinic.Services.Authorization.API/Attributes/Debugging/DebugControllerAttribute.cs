using InnowiseClinic.Services.Authorization.API.Filters.Debugging;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Services.Authorization.API.Attributes.Debugging;

public class DebugControllerAttribute : TypeFilterAttribute
{
    public DebugControllerAttribute() : base(typeof(DebugControllerFilter)) { }
}