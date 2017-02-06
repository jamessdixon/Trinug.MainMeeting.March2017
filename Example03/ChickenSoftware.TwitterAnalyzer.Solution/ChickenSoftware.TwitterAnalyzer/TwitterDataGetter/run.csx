#load "Dtos.csx"
#r "Newtonsoft.Json"

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Collections.Generic;

public static string Encode(String accessToken, String accessTokenSecret)
{
    return Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(accessToken) +
        ":" + Uri.EscapeDataString(accessTokenSecret)));
}

public static String GetAccessToken()
{
    var consumerKey = Environment.GetEnvironmentVariable("consumerKey");
    var consumerSecret = Environment.GetEnvironmentVariable("consumerSecret");

    var uri = @"https://api.twitter.com/oauth2/token";
    var headerFormat = "Basic {0}";
    var encodedConsumerInfo = Encode(consumerKey, consumerSecret);
    var authHeader = String.Format(headerFormat, encodedConsumerInfo);

    var postBody = "grant_type=client_credentials";
    ServicePointManager.Expect100Continue = false;
    var request = (HttpWebRequest)WebRequest.Create(uri);
    request.Headers.Add("Authorization", authHeader);
    request.Method = "POST";
    request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

    var requestStream = request.GetRequestStream();
    var content = ASCIIEncoding.ASCII.GetBytes(postBody);
    requestStream.Write(content, 0, content.Length);

    request.Headers.Add("Accept-Encoding", "gzip");
    var response = (HttpWebResponse)request.GetResponse();
    var responseStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
    var reader = new StreamReader(responseStream);
    var responseJson = reader.ReadToEnd();
    var token = JsonConvert.DeserializeObject<BearerToken>(responseJson);
    return token.access_token;

}

public static String GetQueryResult()
{
    var  searchItem = Environment.GetEnvironmentVariable("searchItem");
    var accessToken = GetAccessToken();
    var uri = @"https://api.twitter.com/1.1/search/tweets.json?q=%40"+ searchItem;
    var headerFormat = "Bearer {0}";
    var authHeader = String.Format(headerFormat, accessToken);
    ServicePointManager.Expect100Continue = false;
    var request = (HttpWebRequest)WebRequest.Create(uri);
    request.Headers.Add("Authorization", authHeader);
    request.Method = "GET";
    var response = (HttpWebResponse)request.GetResponse();
    var responseStream = response.GetResponseStream();
    var reader = new StreamReader(responseStream);
    var responseJson = reader.ReadToEnd();
    return responseJson;

}


public static void Run(TimerInfo timerInfo, out string queueItem)
{
    var results = GetQueryResult();
    RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(results);
    var lastTweet = (from status in rootObject.statuses
                     orderby status.id descending
                     select status).FirstOrDefault();
    queueItem = JsonConvert.SerializeObject(lastTweet);
}