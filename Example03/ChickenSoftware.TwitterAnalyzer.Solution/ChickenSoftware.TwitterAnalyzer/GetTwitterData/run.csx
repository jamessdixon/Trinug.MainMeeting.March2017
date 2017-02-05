
using System;
using System.Web;


public static void Run(TimerInfo timerInfo, out string queueItem)
{

    var consumerKey = ConfigurationManager.AppSettings.["consumerKey"];
    var consumerSecret = ConfigurationManager.AppSettings.["consumerSecret"];
    var searchItem = "TRINUG";
    //var tokenClient = new WebClient();
    //tokenClient.Uri = "";
    //var barrerToken = tokenClient();

    var searchParameter = Search.GenerateSearchTweetParameter(searchItem);
    searchParameter.Until < -DateTime.Now.AddMinutes(-1.0);
    var tweets = Search.SearchTweets(searchItem);
    foreach (var tweet in tweets)
    {
        queueItem = tweet;
    }
}