using Semifinals.Core.Serialization;
using Semifinals.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Payloads;

public class UpdateTeamRequest
{
    [JsonPropertyName("name")]
    [JsonConverter(typeof(TeamNameConverter))]
    public TeamName? Name { get; init; }
}
