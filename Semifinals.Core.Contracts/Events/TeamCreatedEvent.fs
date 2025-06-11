namespace Semifinals.Core.Contracts.Events

open Semifinals.Rosters.Domain.Entities
open System

type TeamCreatedEvent = {
    Team: Team
    Timestamp: DateTime
}
