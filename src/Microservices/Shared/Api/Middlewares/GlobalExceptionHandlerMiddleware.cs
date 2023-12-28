using System.Text;
using Microsoft.AspNetCore.Http;

namespace InnowiseClinic.Microservices.Shared.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IReadOnlyDictionary<Type, int> _exceptionToStatusCodeMap;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, IReadOnlyDictionary<Type, int> exceptionToStatusCodeMap)
    {
        _next = next;
        _exceptionToStatusCodeMap = exceptionToStatusCodeMap;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            if (_exceptionToStatusCodeMap.TryGetValue(e.GetType(), out int statusCode))
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