module Semifinals.Rosters.Presentation.Error

open Semifinals.Rosters.Contracts.Payloads

// General purpose
let BadRequest = ErrorResponse("",  "")
let InternalServerError = ErrorResponse("", "")

// Players
let PlayerNotFound = ErrorResponse("", "")
let UsernameInUse = ErrorResponse("", "")

// TODO: Populate errors once format determined
