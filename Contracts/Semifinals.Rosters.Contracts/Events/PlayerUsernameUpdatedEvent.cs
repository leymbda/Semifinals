using Semifinals.Core.Serialization;
using Semifinals.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Events;

public class PlayerUsernameUpdatedEvent
{
    [JsonPropertyName("player_id")]
    [JsonConverter(typeof(PlayerIdConverter))]
    public required PlayerId PlayerId { get; init; }

    [JsonPropertyName("old_username")]
    [JsonConverter(typeof(UsernameConverter))]
    public required Username OldUsername { get; init; }

    [JsonPropertyName("new_username")]
    [JsonConverter(typeof(UsernameConverter))]
    public required Username NewUsername{ get; init; }

    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(TimestampConverter))]
    public required DateTime Timestamp { get; init; }
}
