module Semifinals.Rosters.Presentation.Error

open Semifinals.Rosters.Contracts.Payloads

// General purpose
let BadRequest = ErrorResponse(Code = "", Message = "")

// Players
let PlayerNotFound = ErrorResponse(Code = "", Message = "")
let UsernameInUse = ErrorResponse(Code = "", Message = "")

// TODO: Populate errors once format determined
