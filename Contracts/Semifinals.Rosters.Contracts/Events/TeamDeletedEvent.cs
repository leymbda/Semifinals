using Semifinals.Core.Serialization;
using Semifinals.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Events;

public class TeamDeletedEvent
{
    [JsonPropertyName("team_id")]
    [JsonConverter(typeof(TeamIdConverter))]
    public required TeamId TeamId { get; init; }

    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(TimestampConverter))]
    public required DateTime Timestamp { get; init; }
}
