namespace Semifinals.Rosters.Application.Services

open FsToolkit.ErrorHandling
open Semifinals.Core.ValueObjects
open Semifinals.Rosters.Domain.Entities
open Semifinals.Rosters.Domain.Repositories
open Semifinals.Rosters.Domain.Services
open System

// TODO: How can service errors be handled better?

[<RequireQualifiedAccess>]
type CreatePlayerError =
    | UsernameUniquenessServiceError of UsernameUniquenessServiceError
    | PlayerRepositoryError of string
    | UsernameInUse

[<RequireQualifiedAccess>]
type UpdatePlayerError =
    | PlayerNotFound
    | UsernameUniquenessServiceError of UsernameUniquenessServiceError
    | PlayerRepositoryError of string
    | UsernameInUse

[<RequireQualifiedAccess>]
type DeletePlayerError =
    | PlayerNotFound
    | PlayerRepositoryError of string

type PlayerService(players: IPlayerRepository, usernameUniqueness: IUsernameUniquenessService) =
    member _.CreatePlayer username displayName country = asyncResult {
        do!
            usernameUniqueness.IsInUse username
            |> AsyncResult.mapError CreatePlayerError.UsernameUniquenessServiceError
            |> AsyncResult.bindRequireFalse CreatePlayerError.UsernameInUse
            |> AsyncResult.ignore

        let id = PlayerId.create (Guid.NewGuid())
        let player = Player.create id username displayName country

        return!
            players.Create player
            |> AsyncResult.mapError CreatePlayerError.PlayerRepositoryError

        // TODO: Send event for player creation
    }

    member _.UpdatePlayer id username displayName country = asyncResult {
        let! player =
            players.GetById id
            |> AsyncResult.mapError UpdatePlayerError.PlayerRepositoryError
            |> AsyncResult.bindRequireSome UpdatePlayerError.PlayerNotFound

        match username with
        | None -> ()
        | Some username ->
            do!
                usernameUniqueness.IsInUse username
                |> AsyncResult.mapError UpdatePlayerError.UsernameUniquenessServiceError
                |> AsyncResult.bindRequireFalse UpdatePlayerError.UsernameInUse
                |> AsyncResult.ignore

        let updated =
            player
            |> Option.foldBack Player.setUsername username
            |> Option.foldBack Player.setDisplayName displayName
            |> Option.foldBack Player.setCountry country

        return!
            players.Update updated
            |> AsyncResult.mapError UpdatePlayerError.PlayerRepositoryError

        // TODO: Send events for player updates that occurred
    }

    member _.DeletePlayer id = asyncResult {
        do!
            players.DeleteById id
            |> AsyncResult.mapError DeletePlayerError.PlayerRepositoryError
            |> AsyncResult.bindRequireTrue DeletePlayerError.PlayerNotFound

        // TODO: Send event for player deletion
    }
