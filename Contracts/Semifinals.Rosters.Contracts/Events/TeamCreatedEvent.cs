using Semifinals.Core.Serialization;
using Semifinals.Rosters.Contracts.Payloads;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Events;

public class TeamCreatedEvent
{
    [JsonPropertyName("team")]
    public required TeamResponse Team { get; init; }

    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(TimestampConverter))]
    public required DateTime Timestamp { get; init; }
}
