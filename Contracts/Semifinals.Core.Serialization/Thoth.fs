module Semifinals.Core.Serialization.Thoth

open Semifinals.Core.ValueObjects
open Thoth.Json.Net

module Decode =
    let result msg f =
        f >> Result.map Decode.succeed >> Result.defaultValue (Decode.fail msg)

module Country =
    let decoder: Decoder<Country> =
        Decode.string |> Decode.andThen (Decode.result "Failed to decode country" Country.create)

    let encoder: Encoder<Country> =
        Country.value >> Encode.string

module DisplayName =
    let decoder: Decoder<DisplayName> =
        Decode.string |> Decode.andThen (Decode.result "Failed to decode display name" DisplayName.create)

    let encoder: Encoder<DisplayName> =
        DisplayName.value >> Encode.string

module PlayerId =
    let decoder: Decoder<PlayerId> =
        Decode.guid |> Decode.map PlayerId.create
        
    let encoder: Encoder<PlayerId> =
        PlayerId.value >> Encode.guid
        
module TeamId =
    let decoder: Decoder<TeamId> =
        Decode.guid |> Decode.map TeamId.create
        
    let encoder: Encoder<TeamId> =
        TeamId.value >> Encode.guid

module TeamName =
    let decoder: Decoder<TeamName> =
        Decode.string |> Decode.andThen (Decode.result "Failed to decode team name" TeamName.create)

    let encoder: Encoder<TeamName> =
        TeamName.value >> Encode.string
        
module Username =
    let decoder: Decoder<Username> =
        Decode.string |> Decode.andThen (Decode.result "Failed to decode username" Username.create)

    let encoder: Encoder<Username> =
        Username.value >> Encode.string
