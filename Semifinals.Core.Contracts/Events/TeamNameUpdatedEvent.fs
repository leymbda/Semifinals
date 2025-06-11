namespace Semifinals.Core.Contracts.Events

open Semifinals.Core.ValueObjects
open System

type TeamNameUpdatedEvent = {
    TeamId: TeamId
    OldName: TeamName
    NewName: TeamName
    Timestamp: DateTime
}
