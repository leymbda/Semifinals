namespace Semifinals.Core.ValueObjects

open System

type PlayerId = private PlayerId of Guid

module PlayerId =
    let create id =
        PlayerId id

    let value (PlayerId id) =
        id
        
    let toString id =
        value id |> _.ToString()

    let fromString (str: string) =
        match Guid.TryParse str with
        | true, guid -> create guid |> Some
        | _ -> None
