using Semifinals.Core.Serialization;
using Semifinals.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Payloads;

public class UpdatePlayerRequest
{
    [JsonPropertyName("username")]
    [JsonConverter(typeof(UsernameConverter))]
    public Username? Username { get; init; }

    [JsonPropertyName("display_name")]
    [JsonConverter(typeof(DisplayNameConverter))]
    public DisplayName? DisplayName { get; init; }

    [JsonPropertyName("country")]
    [JsonConverter(typeof(CountryConverter))]
    public Country? Country { get; init; }
}
