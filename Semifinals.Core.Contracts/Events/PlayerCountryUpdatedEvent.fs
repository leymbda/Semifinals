namespace Semifinals.Core.Contracts.Events

open Semifinals.Core.ValueObjects
open System

type PlayerCountryUpdatedEvent = {
    PlayerId: PlayerId
    OldCountry: Country
    NewCountry: Country
    Timestamp: DateTime
}
