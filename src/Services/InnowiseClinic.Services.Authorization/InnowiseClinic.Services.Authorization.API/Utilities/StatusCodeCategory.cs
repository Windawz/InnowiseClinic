namespace InnowiseClinic.Services.Authorization.API.Utilities;

public readonly record struct StatusCodeCategory
{
    public static readonly StatusCodeCategory Information = new(100);
    public static readonly StatusCodeCategory Success = new(200);
    public static readonly StatusCodeCategory Redirection = new(300);
    public static readonly StatusCodeCategory ClientError = new(400);
    public static readonly StatusCodeCategory ServerError = new(500);
    private readonly int _start;
    
    private StatusCodeCategory(int start)
    {
        _start = start;
    }

    public bool Includes(int statusCode)
    {
        return statusCode <= _start && statusCode < _start + 100;
    }
}