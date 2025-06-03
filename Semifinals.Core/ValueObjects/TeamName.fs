namespace Semifinals.Core.ValueObjects

type TeamName = private TeamName of string

[<RequireQualifiedAccess>]
type TeamNameError =
    | Empty
    | TooShort of min: int
    | TooLong of max: int

module TeamName =
    let private minLength = 1
    let private maxLength = 32

    let private validate (str: string) =
        match str.Trim() with
        | "" -> Error TeamNameError.Empty
        | v when v.Length < minLength -> Error (TeamNameError.TooShort minLength)
        | v when v.Length > maxLength -> Error (TeamNameError.TooLong maxLength)
        | v -> Ok v

    let create str =
        str
        |> validate
        |> Result.map TeamName

    let value (TeamName name) =
        name
