#r "Newtonsoft.Json"

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Collections.Generic;

public class BearerToken 
{
    public string token_type { get; set; }
    public string access_token { get; set; }
}

public class Hashtag
{
    public string text { get; set; }
    public List<int> indices { get; set; }
}

public class Entities
{
    public List<object> urls { get; set; }
    public List<Hashtag> hashtags { get; set; }
    public List<object> user_mentions { get; set; }
}

public class Metadata
{
    public string iso_language_code { get; set; }
    public string result_type { get; set; }
}

public class Url2
{
    public object expanded_url { get; set; }
    public string url { get; set; }
    public List<int> indices { get; set; }
}

public class Url
{
    public List<Url2> urls { get; set; }
}

public class Description
{
    public List<object> urls { get; set; }
}

public class Entities2
{
    public Url url { get; set; }
    public Description description { get; set; }
}

public class User
{
    public string profile_sidebar_fill_color { get; set; }
    public string profile_sidebar_border_color { get; set; }
    public bool? profile_background_tile { get; set; }
    public string name { get; set; }
    public string profile_image_url { get; set; }
    public string created_at { get; set; }
    public string location { get; set; }
    public object follow_request_sent { get; set; }
    public string profile_link_color { get; set; }
    public bool? is_translator { get; set; }
    public string id_str { get; set; }
    public Entities2 entities { get; set; }
    public bool default_profile { get; set; }
    public bool contributors_enabled { get; set; }
    public int favourites_count { get; set; }
    public string url { get; set; }
    public string profile_image_url_https { get; set; }
    public int? utc_offset { get; set; }
    public Int64? id { get; set; }
    public bool profile_use_background_image { get; set; }
    public int listed_count { get; set; }
    public string profile_text_color { get; set; }
    public string lang { get; set; }
    public int? followers_count { get; set; }
    public bool @protected { get; set; }
    public object notifications { get; set; }
    public string profile_background_image_url_https { get; set; }
    public string profile_background_color { get; set; }
    public bool verified { get; set; }
    public bool geo_enabled { get; set; }
    public string time_zone { get; set; }
    public string description { get; set; }
    public bool default_profile_image { get; set; }
    public string profile_background_image_url { get; set; }
    public int? statuses_count { get; set; }
    public int? friends_count { get; set; }
    public object following { get; set; }
    public bool? show_all_inline_media { get; set; }
    public string screen_name { get; set; }
}

public class Status
{
    public object coordinates { get; set; }
    public bool favorited { get; set; }
    public bool truncated { get; set; }
    public string created_at { get; set; }
    public string id_str { get; set; }
    public Entities entities { get; set; }
    public object in_reply_to_user_id_str { get; set; }
    public object contributors { get; set; }
    public string text { get; set; }
    public Metadata metadata { get; set; }
    public int? retweet_count { get; set; }
    public object in_reply_to_status_id_str { get; set; }
    public object id { get; set; }
    public object geo { get; set; }
    public bool retweeted { get; set; }
    public object in_reply_to_user_id { get; set; }
    public object place { get; set; }
    public User user { get; set; }
    public object in_reply_to_screen_name { get; set; }
    public string source { get; set; }
    public object in_reply_to_status_id { get; set; }
}

public class SearchMetadata
{
    public long max_id { get; set; }
    public long since_id { get; set; }
    public string refresh_url { get; set; }
    public string next_results { get; set; }
    public int? count { get; set; }
    public double completed_in { get; set; }
    public string since_id_str { get; set; }
    public string query { get; set; }
    public string max_id_str { get; set; }
}

public class RootObject
{
    public List<Status> statuses { get; set; }
    public SearchMetadata search_metadata { get; set; }
}



public static string Encode(String accessToken, String accessTokenSecret)
{
    return Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(accessToken) + ":" + Uri.EscapeDataString(accessTokenSecret)));
}

public static String GetAccessToken()
{
    var consumerKey = "aMskoPzKPkYxVgW29wNTqw";
    var consumerSecret = "EfQPKQIi2gWNmgJgzPBCabQcfaoEn5tcKXxjDdnyk";

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
    var  searchItem = "jamie_dixon";
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