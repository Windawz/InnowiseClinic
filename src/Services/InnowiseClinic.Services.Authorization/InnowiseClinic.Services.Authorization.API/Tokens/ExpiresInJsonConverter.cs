using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InnowiseClinic.Services.Authorization.API.Tokens;

/// <summary>
/// Converts a <see cref="TimeSpan"/> value into
/// its suitable representation for the "expires_in" field
/// of a token response.
/// </summary>
internal class ExpiresInJsonConverter : JsonConverter<TimeSpan>
{
    /// <inheritdoc/>
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeSpan.FromSeconds(reader.GetInt32());
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Seconds);
    }
}