using Semifinals.Core.Serialization;
using Semifinals.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Payloads;

public class TeamResponse
{
    [JsonPropertyName("id")]
    [JsonConverter(typeof(TeamIdConverter))]
    public required TeamId Id { get; init; }

    [JsonPropertyName("name")]
    [JsonConverter(typeof(TeamNameConverter))]
    public required TeamName Name { get; init; }
}
