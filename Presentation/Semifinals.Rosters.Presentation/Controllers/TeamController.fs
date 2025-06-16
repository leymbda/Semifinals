namespace Semifinals.Rosters.Presentation

open Microsoft.Azure.Functions.Worker
open Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes
open Microsoft.Azure.Functions.Worker.Http
open Semifinals.Rosters.Contracts.Payloads
open System
open System.Net

type TeamController() =
    [<Function "CreateTeam">]
    [<OpenApiOperation("CreateTeam", Description = "Create a team")>]
    [<OpenApiRequestBody(JsonContentType, typeof<CreateTeamRequest>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.Created, JsonContentType, typeof<TeamResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.BadRequest, JsonContentType, typeof<ErrorResponse>)>]
    member _.Post(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "teams")>] req: HttpRequestData,
        [<FromBody>] body: CreateTeamRequest
    ) =
        req.CreateResponse HttpStatusCode.NotImplemented
        // TODO: Create new team
        
    [<Function "GetTeam">]
    [<OpenApiOperation("GetTeam", Description = "Fetch a team")>]
    [<OpenApiResponseWithBody(HttpStatusCode.OK, JsonContentType, typeof<TeamResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.NotFound, JsonContentType, typeof<ErrorResponse>)>]
    member _.Get(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "teams/{teamId:guid}")>] req: HttpRequestData,
        teamId: Guid
    ) =
        req.CreateResponse HttpStatusCode.NotImplemented
        // TODO: Fetch team if exists

    [<Function "UpdateTeam">]
    [<OpenApiOperation("UpdateTeam", Description = "Update a team")>]
    [<OpenApiRequestBody(JsonContentType, typeof<UpdateTeamRequest>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.OK, JsonContentType, typeof<TeamResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.BadRequest, JsonContentType, typeof<ErrorResponse>)>]
    [<OpenApiResponseWithBody(HttpStatusCode.NotFound, JsonContentType, typeof<ErrorResponse>)>]
    member _.Patch(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "teams/{teamId:guid}")>] req: HttpRequestData,
        [<FromBody>] body: UpdateTeamRequest,
        teamId: Guid    
    ) =
        req.CreateResponse HttpStatusCode.NotImplemented
        // TODO: Update team if exists and update provided values
        
    [<Function "DeleteTeam">]
    [<OpenApiOperation("DeleteTeam", Description = "Delete a team")>]
    [<OpenApiResponseWithoutBody(HttpStatusCode.NoContent)>]
    [<OpenApiResponseWithBody(HttpStatusCode.NotFound, JsonContentType, typeof<ErrorResponse>)>]
    member _.Delete(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "teams/{teamId:guid}")>] req: HttpRequestData,
        teamId: Guid
    ) =
        req.CreateResponse HttpStatusCode.NotImplemented
        // TODO: Delete team if exists
        
    [<Function "ListTeams">]
    [<OpenApiOperation("ListTeams", Description = "Get a list of teams using a given query")>]
    [<OpenApiResponseWithBody(HttpStatusCode.OK, JsonContentType, typeof<TeamResponse list>)>]
    member _.Get(
        [<HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "teams")>] req: HttpRequestData
    ) =
        req.CreateResponse HttpStatusCode.NotImplemented
        // TODO: Lists teams based on search query params
        // TODO: Consider supporting multiple query params at the same time (?)
        // TODO: Consider fuzzy search for display name
        // TODO: Consider pagination for handling broader searches
