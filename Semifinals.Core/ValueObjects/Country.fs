namespace Semifinals.Core.ValueObjects

open System.Globalization

type Country = private Country of string

[<RequireQualifiedAccess>]
type CountryError =
    | Unknown
    | InvalidName of expected: string

module Country =
    let private validate (str: string) =
        try
            let region = RegionInfo(str)

            match region.TwoLetterISORegionName = str with
            | true -> Ok str
            | false -> Error (CountryError.InvalidName region.TwoLetterISORegionName)

        with ex -> Error CountryError.Unknown

    let create str =
        str
        |> validate
        |> Result.map Country

    let value (Country country) =
        country

    let region (Country country) =
        RegionInfo(country)
