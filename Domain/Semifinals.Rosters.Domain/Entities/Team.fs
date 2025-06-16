namespace Semifinals.Rosters.Domain.Entities

open Semifinals.Core.ValueObjects

type Team = {
    Id: TeamId
    Name: TeamName
}

module Team =
    let create id name =
        {
            Id = id
            Name = name
        }

    let setName name team =
        { team with Name = name }
