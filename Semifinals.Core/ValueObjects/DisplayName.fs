namespace Semifinals.Core.ValueObjects

type DisplayName = private DisplayName of string

[<RequireQualifiedAccess>]
type DisplayNameError =
    | Empty
    | TooShort of min: int
    | TooLong of max: int

module DisplayName =
    let private minLength = 1
    let private maxLength = 32

    let private validate (str: string) =
        match str.Trim() with
        | "" -> Error DisplayNameError.Empty
        | v when v.Length < minLength -> Error (DisplayNameError.TooShort minLength)
        | v when v.Length > maxLength -> Error (DisplayNameError.TooLong maxLength)
        | v -> Ok v

    let create str =
        str
        |> validate
        |> Result.map DisplayName

    let value (DisplayName displayName) =
        displayName
