using Semifinals.Core.Serialization;
using Semifinals.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Events;

public class PlayerDisplayNameUpdatedEvent
{
    [JsonPropertyName("player_id")]
    [JsonConverter(typeof(PlayerIdConverter))]
    public required PlayerId PlayerId { get; init; }

    [JsonPropertyName("old_display_name")]
    [JsonConverter(typeof(DisplayNameConverter))]
    public required DisplayName OldDisplayName { get; init; }

    [JsonPropertyName("new_display_name")]
    [JsonConverter(typeof(DisplayNameConverter))]
    public required DisplayName NewDisplayName { get; init; }

    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(TimestampConverter))]
    public required DateTime Timestamp { get; init; }
}
