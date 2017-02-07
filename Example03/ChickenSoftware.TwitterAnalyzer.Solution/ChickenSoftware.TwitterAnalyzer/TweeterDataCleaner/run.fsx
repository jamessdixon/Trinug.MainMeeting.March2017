
#r "System.Net.Http"
#r "Newtonsoft.Json"
#r "Microsoft.WindowsAzure.Storage"

open System
open System.Net
open System.Linq
open System.Net.Http
open Newtonsoft.Json
open Newtonsoft.Json.Linq
open Microsoft.WindowsAzure.Storage.Table

type TweetLite() =
    inherit TableEntity()
    member val id: string = null with get, set
    member val user_name: string = null with get, set    
    member val text: string = null with get, set    
    member val created_at: string = null with get, set    
    member val retweet_count: string = null with get, set      

let inline getValue (value:JToken) = 
    match isNull value with
    | false -> value.Value<string>()  
    | true -> ""

let hydrate data = 
    let json = JObject.Parse(data)
    let tweetLite = TweetLite()
    tweetLite.PartitionKey <- "TRINUG"
    tweetLite.RowKey <- Guid.NewGuid().ToString()
    tweetLite.id <- getValue json.["id"]
    tweetLite.user_name <- getValue (json.SelectToken("user.name")) 
    tweetLite.text <- getValue json.["text"] 
    tweetLite.created_at <- getValue json.["created_at"] 
    tweetLite.retweet_count <- getValue json.["retweet_count"]
    tweetLite

let Run (queueItem: string, tweetsIn: IQueryable<TweetLite>, tweetsOut: ICollector<TweetLite>) =  
    let newTweet = hydrate queueItem
    let foundTweet =
        tweetsIn
        |> Seq.tryFind(fun t -> t.id = newTweet.id)
    match foundTweet with
    | Some ft -> ()
    | None ->
        tweetsOut.Add(newTweet)

