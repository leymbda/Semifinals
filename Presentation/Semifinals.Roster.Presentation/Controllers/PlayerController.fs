namespace Semifinals.Rosters.Presentation

open Microsoft.Azure.Functions.Worker
open Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes
open Microsoft.Azure.Functions.Worker.Http
open Semifinals.Rosters.Contracts.Payloads
open System
open System.Net

type PlayerController() =
    [<Function "CreatePlayer">]
    [<OpenApiOperation("CreatePlayer", Description = "Register a new player")>]
    [<OpenApiRequestBody(JsonContentType, typeof<CreatePlayerRequest>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.Created, JsonContentType, typeof<PlayerResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.BadRequest, JsonContentType, typeof<ErrorResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.Conflict, JsonContentType, typeof<ErrorResponse>, Description = "Username already in use")>]
    member _.Post(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "players")>] req: HttpRequestData,
        [<FromBody>] body: CreatePlayerRequest
    ) =
        req.CreateResponse HttpStatusCode.NotImplemented
        // TODO: Create new player if username available
        
    [<Function "GetPlayer">]
    [<OpenApiOperation("GetPlayer", Description = "Fetch a specific player")>]
    [<OpenApiResponseWithBody(HttpStatusCode.OK, JsonContentType, typeof<PlayerResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.NotFound, JsonContentType, typeof<ErrorResponse>)>]
    member _.Get(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "players/{playerId:guid}")>] req: HttpRequestData,
        playerId: Guid
    ) =
        req.CreateResponse HttpStatusCode.NotImplemented
        // TODO: Return given player if exists
        
    [<Function "UpdatePlayer">]
    [<OpenApiOperation("UpdatePlayer", Description = "Update a player")>]
    [<OpenApiRequestBody(JsonContentType, typeof<UpdatePlayerRequest>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.OK, JsonContentType, typeof<PlayerResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.BadRequest, JsonContentType, typeof<ErrorResponse>)>]
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
    ) =
        req.CreateResponse HttpStatusCode.NotImplemented
        // TODO: Delete player if exists
        
    [<Function "ListPlayers">]
    [<OpenApiOperation("ListPlayers", Description = "Get a list of players using a given query")>]
    [<OpenApiResponseWithBody(HttpStatusCode.OK, JsonContentType, typeof<PlayerResponse list>)>]
    member _.Get(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "players")>] req: HttpRequestData
    ) =
        req.CreateResponse HttpStatusCode.NotImplemented
        // TODO: Username availability through ?username=foo
        // TODO: Lists like players from specific region through ?country=bar
        // TODO: Consider supporting multiple query params at the same time (?)
        // TODO: Consider fuzzy search for display/username for searching for players
        // TODO: Consider pagination for handling broader searches like by country
