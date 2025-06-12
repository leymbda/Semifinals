namespace Semifinals.Rosters.Infrastructure.Persistence.Models

open FsToolkit.ErrorHandling
open Semifinals.Core.ValueObjects
open Semifinals.Rosters.Domain.Entities

type TeamModel = {
    Id: string
    Name: string
}

module TeamModel =
    let fromDomain (domain: Team): TeamModel =
        {
            Id = TeamId.toString domain.Id
            Name = TeamName.value domain.Name
        }

    let toDomain (model: TeamModel): Result<Team, string> = result {
        let! id =
            model.Id
            |> TeamId.fromString
            |> Result.requireSome "Invalid team ID"

        let! name =
            model.Name
            |> TeamName.create
            |> Result.setError "Invalid team name"

        return {
            Id = id
            Name = name
        }
    }
