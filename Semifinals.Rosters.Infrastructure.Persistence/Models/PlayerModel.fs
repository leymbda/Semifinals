namespace Semifinals.Rosters.Infrastructure.Persistence.Models

open FsToolkit.ErrorHandling
open Semifinals.Core.ValueObjects
open Semifinals.Rosters.Domain.Entities

type PlayerModel = {
    Id: string
    Username: string
    DisplayName: string
    Country: string
}

module PlayerModel =
    let fromDomain (domain: Player): PlayerModel =
        {
            Id = PlayerId.toString domain.Id
            Username = Username.value domain.Username
            DisplayName = DisplayName.value domain.DisplayName
            Country = Country.value domain.Country
        }

    let toDomain (model: PlayerModel): Result<Player, string> = result {
        let! id =
            model.Id
            |> PlayerId.fromString
            |> Result.requireSome "Invalid player ID"

        let! username =
            model.Username
            |> Username.create
            |> Result.setError "Invalid player username"

        let! displayName =
            model.DisplayName
            |> DisplayName.create
            |> Result.setError "Invalid player display name"

        let! country =
            model.Country
            |> Country.create
            |> Result.setError "Invalid player country code"

        return {
            Id = id
            Username = username
            DisplayName = displayName
            Country = country
        }
    }
