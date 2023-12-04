using Microsoft.AspNetCore.Mvc;

namespace InnowiseClinic.Services.Authorization.API.Debugging;

public class DebugControllerAttribute : TypeFilterAttribute
{
    public DebugControllerAttribute() : base(typeof(DebugControllerFilter)) { }
}