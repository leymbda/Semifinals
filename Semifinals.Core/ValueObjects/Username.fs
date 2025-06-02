namespace Semifinals.Core.ValueObjects

open System.Text.RegularExpressions

type Username = private Username of string

[<RequireQualifiedAccess>]
type UsernameError =
    | Empty
    | TooShort of min: int
    | TooLong of max: int
    | InvalidCharacters of invalid: char list

module Username =
    let private minLength = 3
    let private maxLength = 32
    let private validChars = "[a-z0-9_-]"

    let private findInvalidChars (str: string) =
        str
        |> String.filter (string >> Regex(validChars).IsMatch)
        |> _.ToCharArray()
        |> Array.toList

    let private validate (str: string) =
        let trimmed = str.Trim()
        let invalidChars = findInvalidChars trimmed

        match trimmed with
        | "" -> Error UsernameError.Empty
        | v when v.Length < minLength -> Error (UsernameError.TooShort minLength)
        | v when v.Length > maxLength -> Error (UsernameError.TooLong maxLength)
        | _ when invalidChars |> (not << List.isEmpty) -> Error (UsernameError.InvalidCharacters invalidChars)
        | v -> Ok v

    let create str =
        str
        |> validate
        |> Result.map Username

    let value (Username username) =
        username
