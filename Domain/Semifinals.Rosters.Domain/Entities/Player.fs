namespace Semifinals.Rosters.Domain.Entities

open Semifinals.Core.ValueObjects

type Player = {
    Id: PlayerId
    Username: Username
    DisplayName: DisplayName
    Country: Country
}

module Player =
    let create id username displayName country =
        {
            Id = id
            Username = username
            DisplayName = displayName
            Country = country
        }

    let setUsername username player =
        { player with Username = username }

    let setDisplayName displayName player =
        { player with DisplayName = displayName }

    let setCountry country player =
        { player with Country = country }
