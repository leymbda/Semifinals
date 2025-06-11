namespace Semifinals.Rosters.Domain.Repositories

open Semifinals.Core.ValueObjects
open Semifinals.Rosters.Domain.Entities

type IPlayerRepository =
    abstract member GetById: id: PlayerId -> Async<Result<Player option, string>>
    abstract member GetByUsername: username: Username -> Async<Result<Player option, string>>
    abstract member GetByTeamId: teamId: TeamId -> Async<Result<Player list, string>>
    abstract member Create: player: Player -> Async<Result<Player, string>>
    abstract member Update: player: Player -> Async<Result<Player, string>>
    abstract member DeleteById: id: PlayerId -> Async<Result<bool, string>>
