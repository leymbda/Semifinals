namespace Semifinals.Core.Events

open Semifinals.Core.Entities
open System

type TeamCreatedEvent = {
    Team: Team
    Timestamp: DateTime
}
