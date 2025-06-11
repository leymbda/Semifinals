namespace Semifinals.Core.Contracts.Events

open Semifinals.Rosters.Domain.Entities
open System

type PlayerCreatedEvent = {
    Player: Player
    Timestamp: DateTime
}
