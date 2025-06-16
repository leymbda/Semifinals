using Semifinals.Core.Serialization;
using Semifinals.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Events;

public class PlayerCountryUpdatedEvent
{
    [JsonPropertyName("player_id")]
    [JsonConverter(typeof(PlayerIdConverter))]
    public required PlayerId PlayerId { get; init; }
    
    [JsonPropertyName("old_country")]
    [JsonConverter(typeof(CountryConverter))]
    public required Country OldCountry { get; init; }
    
    [JsonPropertyName("new_country")]
    [JsonConverter(typeof(CountryConverter))]
    public required Country NewCountry { get; init; }

    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(TimestampConverter))]
    public required DateTime Timestamp { get; init; }
}
