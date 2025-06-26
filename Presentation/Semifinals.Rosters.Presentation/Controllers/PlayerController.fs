namespace Semifinals.Rosters.Presentation

open Microsoft.Azure.Functions.Worker
open Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes
open Microsoft.Azure.Functions.Worker.Http
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
    member _.Patch(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "players/{playerId:guid}")>] req: HttpRequestData,
        playerId: Guid,
        [<FromBody>] body: UpdatePlayerRequest
    ) =
        req.CreateResponse HttpStatusCode.NotImplemented
        // TODO: Update player if exists (and confirm username availability if included in request)
        
    [<Function "DeletePlayer">]
    [<OpenApiOperation("DeletePlayer", Description = "Delete a specific player")>]
    [<OpenApiResponseWithoutBody(HttpStatusCode.NoContent)>]
    [<OpenApiResponseWithBody(HttpStatusCode.NotFound, JsonContentType, typeof<ErrorResponse>)>]
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
    [<OpenApiOperation("ListPlayers", Description = "Get a list of players using a given query")>]
    [<OpenApiResponseWithBody(HttpStatusCode.OK, JsonContentType, typeof<PlayerResponse list>)>]
    member _.Get(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "players")>] req: HttpRequestData
    ) =
        req.CreateResponse HttpStatusCode.NotImplemented
        // TODO: Username availability through ?username=foo
        // TODO: Lists players from specific region through ?country=bar
        // TODO: Consider supporting multiple query params at the same time (?)
        // TODO: Consider fuzzy search for display/username for searching for players
        // TODO: Consider pagination for handling broader searches like by country
