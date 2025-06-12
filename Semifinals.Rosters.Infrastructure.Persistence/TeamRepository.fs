namespace Semifinals.Rosters.Infrastructure.Persistence

open FsToolkit.ErrorHandling
open Microsoft.Azure.Cosmos
open Microsoft.Extensions.Options
open Semifinals.Core.ValueObjects
open Semifinals.Rosters.Domain.Repositories
open Semifinals.Rosters.Infrastructure.Persistence.Models

type TeamRepository(cosmos: CosmosClient, options: IOptions<CosmosOptions>) =
    let cfg = options.Value
    let teams = cosmos.GetContainer(cfg.DatabaseName, cfg.TeamContainerName)
    
    interface ITeamRepository with
        member _.GetById teamId = asyncResult {
            try
                let id = TeamId.toString teamId
                let! res = teams.ReadItemAsync<TeamModel>(id, PartitionKey id)
                return! res.Resource |> TeamModel.toDomain |> Result.map Some
            
            with _ ->
                return None
        }

        member _.Create team = asyncResult {
            try
                let model = TeamModel.fromDomain team
                let! res = teams.CreateItemAsync<TeamModel>(model, PartitionKey model.Id)
                return! res.Resource |> TeamModel.toDomain

            with | _ ->
                return! Error "Team already exists"
        }

        member _.Update team = asyncResult {
            try
                let model = TeamModel.fromDomain team
                let! res = teams.ReplaceItemAsync<TeamModel>(model, model.Id, PartitionKey model.Id)
                return! res.Resource |> TeamModel.toDomain

            with | _ ->
                return! Error "Team does not exist"
        }

        member _.DeleteById teamId = asyncResult {
            try
                let id = TeamId.toString teamId
                let! _ = teams.DeleteItemAsync<TeamModel>(id, PartitionKey id)
                return true

            with | _ ->
                return false

            // TODO: The delete operation result may need to check status code rather than throwing exception on fail
        }
