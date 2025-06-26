namespace Semifinals.Rosters.Application.Services

open FsToolkit.ErrorHandling
open Semifinals.Core.ValueObjects
open Semifinals.Rosters.Domain.Entities
open Semifinals.Rosters.Domain.Repositories
open Semifinals.Rosters.Domain.Services
open System

// TODO: How can service errors be handled better? Need additional info provided in non-prod envs

[<RequireQualifiedAccess>]
type CreatePlayerError =
    | InternalServiceError
    | UsernameInUse

[<RequireQualifiedAccess>]
type GetPlayerByIdError =
    | InternalServiceError
    | PlayerNotFound

[<RequireQualifiedAccess>]
type UpdatePlayerError =
    | InternalServiceError
    | PlayerNotFound
    | UsernameInUse

[<RequireQualifiedAccess>]
type DeletePlayerError =
    | InternalServiceError
    | PlayerNotFound

type IPlayerService =
    abstract member CreatePlayer:
        username: Username ->
        displayName: DisplayName ->
        country: Country ->
        Async<Result<Player, CreatePlayerError>>

    abstract member GetPlayerById:
        id: PlayerId ->
        Async<Result<Player, GetPlayerByIdError>>

    abstract member UpdatePlayer:
        id: PlayerId ->
        username: Username option ->
        displayName: DisplayName option ->
        country: Country option ->
        Async<Result<Player, UpdatePlayerError>>

    abstract member DeletePlayer:
        id: PlayerId ->
        Async<Result<unit, DeletePlayerError>>

    // TODO: Create operations for listing by different types of query (DU for different query type?)

type PlayerService(players: IPlayerRepository, usernameUniqueness: IUsernameUniquenessService) =
    interface IPlayerService with
        member _.CreatePlayer username displayName country = asyncResult {
            do!
                usernameUniqueness.IsInUse username
                |> AsyncResult.setError CreatePlayerError.InternalServiceError
                |> AsyncResult.bindRequireFalse CreatePlayerError.UsernameInUse
                |> AsyncResult.ignore

            let id = PlayerId.create (Guid.NewGuid())
            let player = Player.create id username displayName country

            return!
                players.Create player
                |> AsyncResult.setError CreatePlayerError.InternalServiceError

            // TODO: Send event for player creation
        }

        member _.GetPlayerById id =
            players.GetById id
            |> AsyncResult.setError GetPlayerByIdError.InternalServiceError
            |> AsyncResult.bindRequireSome GetPlayerByIdError.PlayerNotFound

        member _.UpdatePlayer id username displayName country = asyncResult {
            let! player =
                players.GetById id
                |> AsyncResult.setError UpdatePlayerError.InternalServiceError
                |> AsyncResult.bindRequireSome UpdatePlayerError.PlayerNotFound

            match username with
            | None -> ()
            | Some username ->
                do!
                    usernameUniqueness.IsInUse username
                    |> AsyncResult.setError UpdatePlayerError.InternalServiceError
                    |> AsyncResult.bindRequireFalse UpdatePlayerError.UsernameInUse
                    |> AsyncResult.ignore

            let updated =
                player
                |> Option.foldBack Player.setUsername username
                |> Option.foldBack Player.setDisplayName displayName
                |> Option.foldBack Player.setCountry country

            return!
                players.Update updated
                |> AsyncResult.setError UpdatePlayerError.InternalServiceError

            // TODO: Send events for player updates that occurred
        }

        member _.DeletePlayer id = asyncResult {
            do!
                players.DeleteById id
                |> AsyncResult.setError DeletePlayerError.InternalServiceError
                |> AsyncResult.bindRequireTrue DeletePlayerError.PlayerNotFound

            // TODO: Send event for player deletion
        }
