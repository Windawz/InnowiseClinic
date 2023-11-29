namespace InnowiseClinic.Services.Authorization.Services.Models;

public record Password
{
    private readonly string _text = null!;

    public Password(string text)
    {
        Text = text;
    }

    public string Text
    {
        get => _text;
        init
        {
            _text = value.Trim();
        }
    }
}