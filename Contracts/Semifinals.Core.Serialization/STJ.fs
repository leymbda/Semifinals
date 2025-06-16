namespace Semifinals.Core.Serialization

open Semifinals.Core.ValueObjects
open System
open System.Text.Json
open System.Text.Json.Serialization
open Thoth.Json.Net

type TimestampConverter() =
    inherit JsonConverter<DateTime>() with
        override _.Read(reader, _, _) =
            reader.GetInt64() |> DateTimeOffset.FromUnixTimeMilliseconds |> _.UtcDateTime

        override _.Write(writer, value, _) =
            DateTimeOffset value |> _.ToUnixTimeMilliseconds() |> writer.WriteNumberValue

type PrimitiveTokenType =
    | String
    | Double
    | Integer
    | Boolean

type ThothConverter<'T>(type': PrimitiveTokenType, decoder: Decoder<'T>, encoder: Encoder<'T>) =
    inherit JsonConverter<'T>() with
        override _.Read(reader, _, _) =
            let str =
                match type' with
                | String -> reader.GetString()
                | Double -> reader.GetDouble().ToString()
                | Integer -> reader.GetInt32().ToString()
                | Boolean -> reader.GetBoolean().ToString()

            str
            |> Decode.fromString decoder
            |> Result.defaultWith (fun err -> raise (JsonException err))

        override _.Write(writer, value, _) =
            let str =
                encoder value
                |> Encode.toString 0

            match type' with
            | String -> writer.WriteStringValue str
            | Double -> writer.WriteNumberValue (str |> Double.Parse)
            | Integer -> writer.WriteNumberValue (str |> Int32.Parse)
            | Boolean -> writer.WriteBooleanValue (str |> Boolean.Parse)

type CountryConverter() =
    inherit ThothConverter<Country>(
        PrimitiveTokenType.String,
        Thoth.Country.decoder,
        Thoth.Country.encoder
    )
    
type DisplayNameConverter() =
    inherit ThothConverter<DisplayName>(
        PrimitiveTokenType.String,
        Thoth.DisplayName.decoder,
        Thoth.DisplayName.encoder
    )
    
type PlayerIdConverter() =
    inherit ThothConverter<PlayerId>(
        PrimitiveTokenType.String,
        Thoth.PlayerId.decoder,
        Thoth.PlayerId.encoder
    )
    
type TeamIdConverter() =
    inherit ThothConverter<TeamId>(
        PrimitiveTokenType.String,
        Thoth.TeamId.decoder,
        Thoth.TeamId.encoder
    )
    
type TeamNameConverter() =
    inherit ThothConverter<TeamName>(
        PrimitiveTokenType.String,
        Thoth.TeamName.decoder,
        Thoth.TeamName.encoder
    )
    
type UsernameConverter() =
    inherit ThothConverter<Username>(
        PrimitiveTokenType.String,
        Thoth.Username.decoder,
        Thoth.Username.encoder
    )
