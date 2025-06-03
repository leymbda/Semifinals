namespace Semifinals.Core.ValueObjects

open System

type TeamId = private TeamId of Guid

module TeamId =
    let create id =
        TeamId id
