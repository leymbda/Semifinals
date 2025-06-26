module Microsoft.Azure.Functions.Worker.Http.HttpResponseData

open Thoth.Json.Net

let asyncWithObjectAsJson<'T> (obj: 'T) (res: HttpResponseData) = async {
    let status = res.StatusCode
    do! res.WriteAsJsonAsync(obj) |> _.AsTask() |> Async.AwaitTask
    res.StatusCode <- status
    return res
}

let asyncWithJson<'T> (encoder: Encoder<'T>) (value: 'T) (res: HttpResponseData) = async {
    let json = encoder value |> Encode.toString 0 
    do! res.WriteStringAsync json |> Async.AwaitTask
    res.Headers.TryAddWithoutValidation("Content-Type", JsonContentType) |> ignore
    return res
}
