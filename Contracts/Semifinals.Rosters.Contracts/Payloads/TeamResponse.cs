using Semifinals.Core.Serialization;
using Semifinals.Core.ValueObjects;
using Semifinals.Rosters.Domain.Entities;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Payloads;

public class TeamResponse(Team team)
{
    [JsonPropertyName("id")]
    [JsonConverter(typeof(TeamIdConverter))]
    public TeamId Id { get; init; } = team.Id;

    [JsonPropertyName("name")]
    [JsonConverter(typeof(TeamNameConverter))]
    public TeamName Name { get; init; } = team.Name;
}
