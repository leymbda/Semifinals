using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Payloads;

public class ErrorResponse
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }

    [JsonPropertyName("message")]
    public required string Message { get; init; }
}

// TODO: Support specific errors within
// TODO: Move this to a core contracts project
