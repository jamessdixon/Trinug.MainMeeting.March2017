
using System;
using System.Net;


public void Send()
{
    var twitterUri = "https://api.twitter.com/oauth2/token";
    var accessToken = "165481117-wufmGDLMtiVcePDdpJhICdeQ2u9i1ciUlayIWYqQ";
    var accessTokenSecret = "n7f4N09LJkIQ6ZbU1IGLfzE6TVUddKs8WHQN0oevBNCsu";
    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(twitterUri);
    request.Credentials = new NetworkCredential(accessToken, accessTokenSecret);
    request.Timeout = 5000;
    request.Method = "POST";
    request.ContentType = "application/x-www-form-urlencoded";
    request.Headers.Add("grant_type", "client_credentials");
    using (Stream requestStream = request.GetRequestStream())
    {
        using (StreamWriter streamWriter = new StreamWriter(requestStream))
        {
            streamWriter.Write("");
        }
    }
    WebResponse response = request.GetResponse();
    //response.GetResponseStream();
}


public static void Run(TimerInfo timerInfo, out string queueItem)
{
    Send();
    //var consumerKey = ConfigurationManager.AppSettings.["consumerKey"];
    //var consumerSecret = ConfigurationManager.AppSettings.["consumerSecret"];
    //var searchItem = "TRINUG";
    ////var tokenClient = new WebClient();
    ////tokenClient.Uri = "";
    ////var barrerToken = tokenClient();

    //var searchParameter = Search.GenerateSearchTweetParameter(searchItem);
    //searchParameter.Until < -DateTime.Now.AddMinutes(-1.0);
    //var tweets = Search.SearchTweets(searchItem);
    //foreach (var tweet in tweets)
    //{
    //    queueItem = tweet;
    //}
}