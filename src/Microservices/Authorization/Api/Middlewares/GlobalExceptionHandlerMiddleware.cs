using System.Text;
using InnowiseClinic.Microservices.Authorization.Api.Services.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;

namespace InnowiseClinic.Microservices.Authorization.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    private static readonly IReadOnlyDictionary<Type, int> _statusCodeMappings = new Dictionary<Type, int>()
    {
        [typeof(AccountAlreadyExistsException)] = StatusCodes.Status409Conflict,
        [typeof(AccountNotFoundException)] = StatusCodes.Status404NotFound,
        [typeof(InvalidPasswordException)] = StatusCodes.Status400BadRequest,
        [typeof(InvalidRefreshTokenException)] = StatusCodes.Status401Unauthorized,
        [typeof(InvalidRefreshTokenFormatException)] = StatusCodes.Status400BadRequest,
    };

    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            if (_statusCodeMappings.TryGetValue(e.GetType(), out int statusCode))
            {
                context.Response.StatusCode = statusCode;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            
            await context.Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(e.Message + '\n'));
        }
    }
}