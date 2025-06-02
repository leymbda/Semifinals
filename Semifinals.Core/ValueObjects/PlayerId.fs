namespace Semifinals.Core.ValueObjects

open System

type PlayerId = private PlayerId of Guid

module PlayerId =
    let create id =
        PlayerId id

