namespace Semifinals.Core.Contracts.Events

open Semifinals.Core.ValueObjects
open System

type PlayerDisplayNameUpdatedEvent = {
    PlayerId: PlayerId
    OldDisplayName: DisplayName
    NewDisplayName: DisplayName
    Timestamp: DateTime
}
