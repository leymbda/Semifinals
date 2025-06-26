using Semifinals.Core.Serialization;
using Semifinals.Core.ValueObjects;
using Semifinals.Rosters.Domain.Entities;
using System.Text.Json.Serialization;

namespace Semifinals.Rosters.Contracts.Payloads;

public class PlayerResponse(Player player)
{
    [JsonPropertyName("id")]
    [JsonConverter(typeof(PlayerIdConverter))]
    public PlayerId Id { get; init; } = player.Id;

    [JsonPropertyName("username")]
    [JsonConverter(typeof(UsernameConverter))]
    public Username Username { get; init; } = player.Username;

    [JsonPropertyName("display_name")]
    [JsonConverter(typeof(DisplayNameConverter))]
    public DisplayName DisplayName { get; init; } = player.DisplayName;

    [JsonPropertyName("country")]
    [JsonConverter(typeof(CountryConverter))]
    public Country Country { get; init; } = player.Country;
}
