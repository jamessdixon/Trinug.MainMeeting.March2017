
open System.Net
open System.Text


let encodeString (currentString:string) =
    let bytes = Encoding.Default.GetBytes(currentString);
    Encoding.UTF8.GetString(bytes)
   
let accessToken = "165481117-wufmGDLMtiVcePDdpJhICdeQ2u9i1ciUlayIWYqQ"
let accessTokenSecret = "n7f4N09LJkIQ6ZbU1IGLfzE6TVUddKs8WHQN0oevBNCsu"
let authString = "Basic " + encodeString(accessToken) + " " + encodeString(accessTokenSecret)


let uri = @"https://api.twitter.com/oauth2/token"
let authClient = new WebClient()
authClient.Headers.Add("Content-Type","application/x-www-form-urlencoded;charset=UTF-8")
authClient.Headers.Add("Authorization",authString);
authClient.Headers.Add("grant_type","client_credentials");
let barrerToken = authClient.UploadString(uri,"")
barrerToken
