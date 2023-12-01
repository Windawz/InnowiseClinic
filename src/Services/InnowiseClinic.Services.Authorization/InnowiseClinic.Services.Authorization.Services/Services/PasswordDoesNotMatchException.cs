namespace InnowiseClinic.Services.Authorization.Services.Services;

public class PasswordDoesNotMatchException : FailedToLogInException
{
    public PasswordDoesNotMatchException(string passwordText) : base("Provided password does not match")
    {
        PasswordText = passwordText;
    }

    public string PasswordText { get; }
}