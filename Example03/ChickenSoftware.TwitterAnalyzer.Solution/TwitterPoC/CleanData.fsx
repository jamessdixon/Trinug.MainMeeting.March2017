
#r "../packages/Newtonsoft.Json.9.0.1/lib/net45/Newtonsoft.Json.dll"
#r "../packages/FSharp.Data.2.3.2/lib/net40/FSharp.Data.dll"

open System
open System.IO
open System.Net
open System.Text
open FSharp.Data
open Newtonsoft.Json
open Newtonsoft.Json.Linq
open System.IO.Compression

type TweetLite() =
    member val id: string = null with get, set
    member val user_name: string = null with get, set    
    member val text: string = null with get, set    
    member val created_at: string = null with get, set    
    member val retweet_count: string = null with get, set    

let inline getValue (value:JToken) = 
    match isNull value with
    | false ->
        value.Value<string>()  
    | true -> ""

let inline getChildValue (value:JToken) = 
    match isNull value with
    | false ->
        value.Value<string>()  
    | true -> ""


let hydrate data = 
    let json = JObject.Parse(data)
    let tweetLite = TweetLite()
    tweetLite.id <- getValue json.["id"]
    tweetLite.user_name <- getValue (json.SelectToken("user.name")) 
    tweetLite.text <- getValue json.["text"] 
    tweetLite.created_at <- getValue json.["created_at"] 
    tweetLite.retweet_count <- getValue json.["retweet_count"]
    tweetLite

let data = File.ReadAllText(__SOURCE_DIRECTORY__ + "\sampleTweet.json")
let data' = hydrate data

//type TweetContext = JsonProvider<"sampleTweet.json">
//let data = TweetContext.Parse("sampleTweet.json")
//data.Id

//let data = "sampleTweet.json"
//let data' = Encoding.UTF8.GetBytes(data)
//let data'' = Encoding.ASCII.GetString(data')

printfn "User Name: %s" data'.user_name
printfn "Id: %s" data'.id




