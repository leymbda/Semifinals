using Semifinals.Core.Serialization;
using Semifinals.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Events;

public class TeamNameUpdatedEvent
{
    [JsonPropertyName("team_id")]
    [JsonConverter(typeof(TeamIdConverter))]
    public required TeamId TeamId { get; init; }

    [JsonPropertyName("old_team_name")]
    [JsonConverter(typeof(TeamNameConverter))]
    public required TeamName OldTeamName { get; init; }
    
    [JsonPropertyName("new_team_name")]
    [JsonConverter(typeof(TeamNameConverter))]
    public required TeamName NewTeamName { get; init; }

    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(TimestampConverter))]
    public required DateTime Timestamp { get; init; }
}
