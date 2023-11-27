using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InnowiseClinic.Services.Authorization.API.Infrastructure;

/// <summary>
/// Maps an exception whose base class is <typeparamref name="TRootException"/>
/// to a status code. Sets the context result to one having the status code set
/// and containing a JSON object with error information.
/// </summary>
/// <remarks>
/// If the exception is not <typeparamref name="TRootException"/> or does not inherit from it,
/// or if no status code is mapped to the exception type or any of its base types, the
/// exception is ignored.
/// </remarks>
/// <typeparam name="TRootException">The base class of exceptions to catch and map.</typeparam>
internal abstract class StatusCodeMappingExceptionFilter<TRootException> : IExceptionFilter where TRootException : Exception
{
    /// <inheritdoc/>
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

    /// <summary>
    /// Returns a suitable error code for the exception, if any.
    /// </summary>
    /// <remarks>
    /// If no status code is returned, the exception is ignored.
    /// </remarks>
    /// <param name="exception">The exception to map to a status code.</param>
    /// <returns>The status code matching the exception, or null if no status code matches it.</returns>
    protected abstract int? MapExceptionToStatusCode(TRootException exception);
}
