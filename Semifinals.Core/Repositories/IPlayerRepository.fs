namespace Semifinals.Core.Repositories

open Semifinals.Core.Entities
open Semifinals.Core.ValueObjects

// TODO: Decide on approach for defining errors in interface to replace units

type IPlayerRepository =
    abstract member GetById: id: PlayerId -> Async<Result<Player option, unit>>
    abstract member GetByUsername: username: Username -> Async<Result<Player option, unit>>
    abstract member Create: player: Player -> Async<Result<Player, unit>>
    abstract member Update: player: Player -> Async<Result<Player, unit>>
    abstract member DeleteById: id: PlayerId -> Async<Result<bool, unit>>
