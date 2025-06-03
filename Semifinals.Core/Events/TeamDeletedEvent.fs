namespace Semifinals.Core.Events

open Semifinals.Core.ValueObjects
open System

type TeamDeletedEvent = {
    TeamId: TeamId
    Timestamp: DateTime
}
