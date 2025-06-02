namespace Semifinals.Core.Events

open Semifinals.Core.ValueObjects
open System

type PlayerDeletedEvent = {
    PlayerId: PlayerId
    Timestamp: DateTime
}
