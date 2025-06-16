using Semifinals.Core.Serialization;
using Semifinals.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Events;

public class PlayerDeletedEvent
{
    [JsonPropertyName("player")]
    [JsonConverter(typeof(PlayerIdConverter))]
    public required PlayerId PlayerId { get; init; }

    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(TimestampConverter))]
    public required DateTime Timestamp { get; init; }
}
