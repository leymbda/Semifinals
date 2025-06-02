namespace Semifinals.Core.Services

open Semifinals.Core.ValueObjects

// TODO: Decide on approach for defining errors in interface to replace units
// TODO: Consider race conditions and being able to lock a username before assigning

type IUsernameUniquenessService =
    abstract member IsInUse: username: Username -> Async<Result<bool, unit>>
