namespace Semifinals.Core.Repositories

open Semifinals.Core.Entities
open Semifinals.Core.ValueObjects

type ITeamRepository =
    abstract member GetById: id: TeamId -> Async<Result<Team option, string>>
    abstract member Create: team: Team -> Async<Result<Team, string>>
    abstract member Update: team: Team -> Async<Result<Team, string>>
    abstract member DeleteById: id: TeamId -> Async<Result<bool, string>>
