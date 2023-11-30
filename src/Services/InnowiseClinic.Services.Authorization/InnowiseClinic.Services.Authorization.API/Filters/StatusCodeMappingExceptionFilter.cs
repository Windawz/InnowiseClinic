using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InnowiseClinic.Services.Authorization.API.Filters;

public abstract class StatusCodeMappingExceptionFilter<TRootException> : IExceptionFilter where TRootException : Exception
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        if (exception is TRootException rootException)
        {
            int? statusCode = MapExceptionToStatusCode(rootException);
            if (statusCode is not null)
            {
                var value = new
                {
                    Error = new
                    {
                        StatusCode = statusCode.Value,
                        Message = exception.Message,
                    },
                };

                context.Result = new ObjectResult(value)
                {
                    StatusCode = statusCode,
                };

                context.ExceptionHandled = true;
            }
        }
    }

    protected abstract int? MapExceptionToStatusCode(TRootException exception);
}
