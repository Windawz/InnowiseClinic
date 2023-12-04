using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InnowiseClinic.Services.Authorization.API.Debugging;

public class VerifyActionOutputTypeFilter : IResultFilter
{
    public void OnResultExecuted(ResultExecutedContext context) { }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult result && result.Value is not null)
        {
            var controllerType = context.Controller.GetType();
            if (controllerType.GetCustomAttribute<DisableActionVerificationAttribute>() is null)
            {
                var actionMethodInfo = (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo;
                if (actionMethodInfo?.GetCustomAttribute<DisableActionVerificationAttribute>() is null)
                {
                    var outputType = result.Value.GetType();
                    if (outputType.GetCustomAttribute<ActionOutputAttribute>() is null)
                    {
                        context.Cancel = true;
                        throw new InvalidActionOutputTypeException(
                            outputType: outputType,
                            actionDescriptor: context.ActionDescriptor);
                    }
                }
            }
        }
    }
}