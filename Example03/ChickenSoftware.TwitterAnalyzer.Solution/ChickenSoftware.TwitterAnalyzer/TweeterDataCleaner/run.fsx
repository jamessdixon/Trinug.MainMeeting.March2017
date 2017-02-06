
#r "Microsoft.WindowsAzure.Storage"

open System
open Microsoft.WindowsAzure.Storage.Table

type TweetLite() =
    inherit TableEntity()
    member val id: int = 0 with get, set
    member val user_name: string = null with get, set    
    member val text: string = null with get, set    
    member val created_at: string = null with get, set    
    member val retweet_count: int = 0 with get, set    

let hydrate data = 
    let json = JObject.Parse(data)
    let tweetLite = TweetLite()
    tweetLite.PartitionKey <- "TRINUG"
    tweetLite.RowKey <- Guid.NewGuid().ToString()
    tweetLite.id <- json.["id"]
    tweetLite.user_name <- json.["lastName"] 
    tweetLite.text <- json.["User.name"] 
    tweetLite.created_at <- json.["created_at"] 
    tweetLite.retweet_count <- json.["retweet_count"] 
    tweetLite

let Run (queueItem: string, tweets: ICollector<Tweet>) =  
    let newTweet = hydrate queueItem
    let foundTweet =
        tweets
        |> Seq.tryFind(fun t -> t.id = newTweet.id)
    match foundTweet with
    | Some ft -> ()
    | None ->
        tweets.add(newTweet)

