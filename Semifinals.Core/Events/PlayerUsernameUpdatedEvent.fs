namespace Semifinals.Core.Events

open Semifinals.Core.ValueObjects
open System

type PlayerUsernameUpdatedEvent = {
    PlayerId: PlayerId
    OldUsername: Username
    NewUsername: Username
    Timestamp: DateTime
}
