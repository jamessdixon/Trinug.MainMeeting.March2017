
#r "../packages/Newtonsoft.Json.9.0.1/lib/net45/Newtonsoft.Json.dll"
#r "../packages/FSharp.Data.2.3.2/lib/net40/FSharp.Data.dll"

open System
open System.IO
open System.Net
open System.Text
open FSharp.Data
open Newtonsoft.Json
open System.IO.Compression


type BearerToken() =
    member val token_type: string = null with get, set    
    member val access_token: string = null with get, set
    
type TweetContext = JsonProvider<"sampleTweets.json">    

let getAccessToken = 
    let encode accessToken accessTokenSecret =
        Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(accessToken) + ":" + Uri.EscapeDataString(accessTokenSecret)))
  
    let consumerKey = "aMskoPzKPkYxVgW29wNTqw"
    let consumerSecret = "EfQPKQIi2gWNmgJgzPBCabQcfaoEn5tcKXxjDdnyk"
    let accessToken = "165481117-wufmGDLMtiVcePDdpJhICdeQ2u9i1ciUlayIWYqQ"
    let accessTokenSecret = "n7f4N09LJkIQ6ZbU1IGLfzE6TVUddKs8WHQN0oevBNCsu"

    let uri = @"https://api.twitter.com/oauth2/token"
    let headerFormat = "Basic {0}"
    let authHeader = String.Format(headerFormat, encode consumerKey consumerSecret)

    let postBody = "grant_type=client_credentials"
    ServicePointManager.Expect100Continue <- false
    let request = WebRequest.Create(uri) :?> HttpWebRequest
    request.Headers.Add("Authorization", authHeader)
    request.Method <- "POST"
    request.ContentType <- "application/x-www-form-urlencoded;charset=UTF-8"

    let requestStream = request.GetRequestStream()
    let content = ASCIIEncoding.ASCII.GetBytes(postBody)
    requestStream.Write(content, 0, content.Length)

    request.Headers.Add("Accept-Encoding", "gzip")
    let response = request.GetResponse() :?> HttpWebResponse

    let responseStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress)
    let reader = new StreamReader(responseStream)
    let responseJson = reader.ReadToEnd()
    let token = JsonConvert.DeserializeObject<BearerToken>(responseJson)
    token.access_token


let getQueryResult token =
    let accessToken = getAccessToken
    let uri = @"https://api.twitter.com/1.1/search/tweets.json?q=%40" + token + "&until="
    let headerFormat = "Bearer {0}"
    let authHeader = String.Format(headerFormat, accessToken)
    ServicePointManager.Expect100Continue <- false
    let request = WebRequest.Create(uri) :?> HttpWebRequest
    request.Headers.Add("Authorization", authHeader)
    request.Method <- "GET"
    let response = request.GetResponse() :?> HttpWebResponse
    let responseStream = response.GetResponseStream()
    let reader = new StreamReader(responseStream)
    let responseJson = reader.ReadToEnd()
    let tweets = TweetContext.Parse(responseJson)
    tweets.Statuses

//https://twitter.com/PatMcCroryNC
//https://twitter.com/RoyCooperNC

getQueryResult "PatMcCroryNC"

