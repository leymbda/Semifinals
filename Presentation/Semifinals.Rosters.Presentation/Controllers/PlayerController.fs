namespace Semifinals.Rosters.Presentation

open Microsoft.Azure.Functions.Worker
open Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes
open Microsoft.Azure.Functions.Worker.Http
open Microsoft.OpenApi.Models
open Semifinals.Core.ValueObjects
open Semifinals.Rosters.Application.Services
open Semifinals.Rosters.Contracts.Payloads
open System
open System.Net

type PlayerController(players: IPlayerService) =
    [<Function "CreatePlayer">]
    [<OpenApiOperation("CreatePlayer", Description = "Register a new player")>]
    [<OpenApiRequestBody(JsonContentType, typeof<CreatePlayerRequest>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.Created, JsonContentType, typeof<PlayerResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.BadRequest, JsonContentType, typeof<ErrorResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.Conflict, JsonContentType, typeof<ErrorResponse>, Description = "Username already in use")>]
    [<OpenApiResponseWithBody(HttpStatusCode.InternalServerError, JsonContentType, typeof<ErrorResponse>)>]
    member _.Post(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "players")>] req: HttpRequestData,
        [<FromBody>] body: CreatePlayerRequest
    ) = async {
        match! players.CreatePlayer body.Username body.DisplayName body.Country with
        | Error CreatePlayerError.InternalServiceError ->
            return!
                req.CreateResponse HttpStatusCode.InternalServerError
                |> HttpResponseData.asyncWithObjectAsJson Error.InternalServerError

        | Error CreatePlayerError.UsernameInUse ->
            return!
                req.CreateResponse HttpStatusCode.Conflict
                |> HttpResponseData.asyncWithObjectAsJson Error.UsernameInUse

        | Ok player ->
            return!
                req.CreateResponse HttpStatusCode.OK
                |> HttpResponseData.asyncWithObjectAsJson (PlayerResponse player)
            
    }
        
    [<Function "GetPlayer">]
    [<OpenApiOperation("GetPlayer", Description = "Fetch a specific player")>]
    [<OpenApiResponseWithBody(HttpStatusCode.OK, JsonContentType, typeof<PlayerResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.NotFound, JsonContentType, typeof<ErrorResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.InternalServerError, JsonContentType, typeof<ErrorResponse>)>]
    member _.Get(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "players/{playerId:guid}")>] req: HttpRequestData,
        playerId: Guid
    ) = async {
        match! players.GetPlayerById (PlayerId.create playerId) with
        | Error GetPlayerByIdError.InternalServiceError ->
            return!
                req.CreateResponse HttpStatusCode.InternalServerError
                |> HttpResponseData.asyncWithObjectAsJson Error.InternalServerError

        | Error GetPlayerByIdError.PlayerNotFound ->
            return!
                req.CreateResponse HttpStatusCode.NotFound
                |> HttpResponseData.asyncWithObjectAsJson Error.PlayerNotFound
                
        | Ok player ->
            return!
                req.CreateResponse HttpStatusCode.OK
                |> HttpResponseData.asyncWithObjectAsJson (PlayerResponse player)
    }
        
    [<Function "UpdatePlayer">]
    [<OpenApiOperation("UpdatePlayer", Description = "Update a player")>]
    [<OpenApiRequestBody(JsonContentType, typeof<UpdatePlayerRequest>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.OK, JsonContentType, typeof<PlayerResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.BadRequest, JsonContentType, typeof<ErrorResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.NotFound, JsonContentType, typeof<ErrorResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.Conflict, JsonContentType, typeof<ErrorResponse>, Description = "Username already in use")>]
    [<OpenApiResponseWithBody(HttpStatusCode.InternalServerError, JsonContentType, typeof<ErrorResponse>)>]
    member _.Patch(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "players/{playerId:guid}")>] req: HttpRequestData,
        playerId: Guid,
        [<FromBody>] body: UpdatePlayerRequest
    ) = async {
        let id = PlayerId.create playerId
        let username = body.Username |> Option.ofObj
        let displayName = body.DisplayName |> Option.ofObj
        let country = body.Country |> Option.ofObj

        match! players.UpdatePlayer id username displayName country with
        | Error UpdatePlayerError.InternalServiceError ->
            return!
                req.CreateResponse HttpStatusCode.InternalServerError
                |> HttpResponseData.asyncWithObjectAsJson Error.InternalServerError

        | Error UpdatePlayerError.PlayerNotFound ->
            return!
                req.CreateResponse HttpStatusCode.NotFound
                |> HttpResponseData.asyncWithObjectAsJson Error.PlayerNotFound
                
        | Error UpdatePlayerError.UsernameInUse ->
            return!
                req.CreateResponse HttpStatusCode.Conflict
                |> HttpResponseData.asyncWithObjectAsJson Error.UsernameInUse

        | Ok player ->
            return!
                req.CreateResponse HttpStatusCode.OK
                |> HttpResponseData.asyncWithObjectAsJson (PlayerResponse player)
    }
        
    [<Function "DeletePlayer">]
    [<OpenApiOperation("DeletePlayer", Description = "Delete a specific player")>]
    [<OpenApiResponseWithoutBody(HttpStatusCode.NoContent)>]
    [<OpenApiResponseWithBody(HttpStatusCode.NotFound, JsonContentType, typeof<ErrorResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.InternalServerError, JsonContentType, typeof<ErrorResponse>)>]
    member _.Delete(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "players/{playerId:guid}")>] req: HttpRequestData,
        playerId: Guid
    ) = async {
        match! players.DeletePlayer (PlayerId.create playerId) with
        | Error DeletePlayerError.InternalServiceError ->
            return!
                req.CreateResponse HttpStatusCode.InternalServerError
                |> HttpResponseData.asyncWithObjectAsJson Error.InternalServerError

        | Error DeletePlayerError.PlayerNotFound ->
            return!
                req.CreateResponse HttpStatusCode.NotFound
                |> HttpResponseData.asyncWithObjectAsJson Error.PlayerNotFound
                
        | Ok () ->
            return req.CreateResponse HttpStatusCode.NoContent
    }
        
    [<Function "ListPlayers">]
    [<OpenApiOperation("ListPlayers", Description = "Get a list of players using a given query (search params are mutually exclusive)")>]
    [<OpenApiParameter("country", In = ParameterLocation.Query, Explode = true, Description = "Search by player home country")>]
    [<OpenApiParameter("name", In = ParameterLocation.Query, Explode = true, Description = "Find users with value as part of their username or display name")>]
    [<OpenApiParameter("username", In = ParameterLocation.Query, Explode = true, Description = "Find users with value as part of their username")>]
    [<OpenApiParameter("display_name", In = ParameterLocation.Query, Explode = true, Description = "Find users with value as part of their display name")>]
    [<OpenApiResponseWithBody(HttpStatusCode.OK, JsonContentType, typeof<PlayerResponse list>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.BadRequest, JsonContentType, typeof<ErrorResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.InternalServerError, JsonContentType, typeof<ErrorResponse>)>]
    member _.Get(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "players")>] req: HttpRequestData
    ) = async {
        let country = req.Query.Get "country" |> Option.ofObj |> Option.map Country.create
        let name = req.Query.Get "name" |> Option.ofObj
        let username = req.Query.Get "username" |> Option.ofObj
        let displayName = req.Query.Get "display_name" |> Option.ofObj

        let query =
            match country, name, username, displayName with
            | Some (Ok country), None, None, None -> Ok (ListPlayersQuery.Country country)
            | None, Some name, None, None -> Ok (ListPlayersQuery.Name name)
            | None, None, Some username, None -> Ok (ListPlayersQuery.Username username)
            | None, None, None, Some displayName -> Ok (ListPlayersQuery.DisplayName displayName)
            | Some (Error _), _, _, _ -> Error Error.InvalidCountryCode
            | _, _, _, _ -> Error Error.TooManySearchParams

        match query with
        | Error err ->
            return!
                req.CreateResponse HttpStatusCode.BadRequest
                |> HttpResponseData.asyncWithObjectAsJson err

        | Ok query ->
            match! players.ListPlayers query with
            | Error ListPlayersError.InternalServiceError ->
                return!
                    req.CreateResponse HttpStatusCode.InternalServerError
                    |> HttpResponseData.asyncWithObjectAsJson Error.InternalServerError

            | Ok players ->
                return!
                    req.CreateResponse HttpStatusCode.OK
                    |> HttpResponseData.asyncWithObjectAsJson players
    }

     // TODO: Consider how to handle proper pagination
     // TODO: Bad requests not properly handled, need middleware?
