using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Payloads;

public class ErrorResponse(string code, string message)
{
    [JsonPropertyName("code")]
    public string Code { get; init; } = code;

    [JsonPropertyName("message")]
    public string Message { get; init; } = message;
}

// TODO: Support specific errors within
// TODO: Move this to a core contracts project
