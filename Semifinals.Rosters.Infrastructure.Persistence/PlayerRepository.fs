namespace Semifinals.Rosters.Infrastructure.Persistence

open FsToolkit.ErrorHandling
open Microsoft.Azure.Cosmos
open Microsoft.Azure.Cosmos.Linq
open Microsoft.Extensions.Options
open Semifinals.Core.ValueObjects
open Semifinals.Rosters.Domain.Repositories
open Semifinals.Rosters.Infrastructure.Persistence.Models
open System.Linq

type PlayerRepository(cosmos: CosmosClient, options: IOptions<CosmosOptions>) =
    let cfg = options.Value
    let players = cosmos.GetContainer(cfg.DatabaseName, cfg.PlayerContainerName)
    
    interface IPlayerRepository with
        member _.GetById playerId = asyncResult {
            try
                let id = PlayerId.toString playerId
                let! res = players.ReadItemAsync<PlayerModel>(id, PartitionKey id)
                return! res.Resource |> PlayerModel.toDomain |> Result.map Some
            
            with _ ->
                return None
        }

        member _.GetByUsername username = asyncResult {
            let! res =
                players.GetItemLinqQueryable<PlayerModel>()
                |> _.Where(fun p -> p.Username = Username.value username)
                |> _.ToFeedIterator()
                |> _.ReadNextAsync()

            try return! res.Resource.First() |> PlayerModel.toDomain |> Result.map Some
            with _ -> return None
        }
        
        member _.GetByTeamId teamId =
            raise (System.NotImplementedException())

            // TODO: TeamId not currently in player model

        member _.Create player = asyncResult {
            try
                let model = PlayerModel.fromDomain player
                let! res = players.CreateItemAsync<PlayerModel>(model, PartitionKey model.Id)
                return! res.Resource |> PlayerModel.toDomain

            with | _ ->
                return! Error "Player already exists"
        }

        member _.Update player = asyncResult {
            try
                let model = PlayerModel.fromDomain player
                let! res = players.ReplaceItemAsync<PlayerModel>(model, model.Id, PartitionKey model.Id)
                return! res.Resource |> PlayerModel.toDomain

            with | _ ->
                return! Error "Player does not exist"
        }

        member _.DeleteById playerId = asyncResult {
            try
                let id = PlayerId.toString playerId
                let! _ = players.DeleteItemAsync<PlayerModel>(id, PartitionKey id)
                return true

            with | _ ->
                return false

            // TODO: The delete operation result may need to check status code rather than throwing exception on fail
        }
