namespace Semifinals.Core.Events

open Semifinals.Core.Entities
open System

type PlayerCreatedEvent = {
    Player: Player
    Timestamp: DateTime
}
