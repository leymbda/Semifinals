namespace Semifinals.Core.ValueObjects

open System

type TeamId = private TeamId of Guid

module TeamId =
    let create id =
        TeamId id
        
    let value (TeamId id) =
        id

    let toString id =
        value id |> _.ToString()
        
    let fromString (str: string) =
        match Guid.TryParse str with
        | true, guid -> create guid |> Some
        | _ -> None
