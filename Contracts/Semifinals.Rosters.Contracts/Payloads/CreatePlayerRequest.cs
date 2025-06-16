using Semifinals.Core.Serialization;
using Semifinals.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Payloads;

public class CreatePlayerRequest
{
    [JsonPropertyName("username")]
    [JsonConverter(typeof(UsernameConverter))]
    public required Username Username { get; init; }

    [JsonPropertyName("display_name")]
    [JsonConverter(typeof(DisplayNameConverter))]
    public required DisplayName DisplayName { get; init; }

    [JsonPropertyName("country")]
    [JsonConverter(typeof(CountryConverter))]
    public required Country Country { get; init; }
}
