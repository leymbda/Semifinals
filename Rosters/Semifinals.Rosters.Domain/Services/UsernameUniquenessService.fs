namespace Semifinals.Rosters.Domain.Services

open FsToolkit.ErrorHandling
open Semifinals.Core.ValueObjects
open Semifinals.Rosters.Domain.Repositories

type UsernameUniquenessServiceError =
    | DataAccessError of string

type IUsernameUniquenessService =
    abstract member IsInUse: username: Username -> Async<Result<bool, UsernameUniquenessServiceError>>

type UsernameUniquenessService(players: IPlayerRepository) =
    interface IUsernameUniquenessService with
        member _.IsInUse username =
            players.GetByUsername username
            |> AsyncResult.mapError UsernameUniquenessServiceError.DataAccessError
            |> AsyncResult.map Option.isSome
