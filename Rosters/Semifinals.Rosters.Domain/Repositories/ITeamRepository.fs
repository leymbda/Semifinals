namespace Semifinals.Rosters.Domain.Repositories

open Semifinals.Core.ValueObjects
open Semifinals.Rosters.Domain.Entities

type ITeamRepository =
    abstract member GetById: id: TeamId -> Async<Result<Team option, string>>
    abstract member Create: team: Team -> Async<Result<Team, string>>
    abstract member Update: team: Team -> Async<Result<Team, string>>
    abstract member DeleteById: id: TeamId -> Async<Result<bool, string>>
