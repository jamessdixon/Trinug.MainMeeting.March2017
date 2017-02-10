
var fs = require('fs');
fs.readFile('sentiment.json', 'utf8', function (err, blob) {
    var analysis = JSON.parse(blob);
    var body = "<!DOCTYPE html> <html> <head> </head> <body> ";
    body += "<b>Overall sentiment score = " + analysis.totalSentiment + "</b>";
    var positiveTweets = analysis.data.filter(function (x) { return x.sentiment >= 75 });
    var negativeTweets = analysis.data.filter(function (x) { return x.sentiment <= 25 });
    var neutralTweets = analysis.data.filter(function (x) { return x.sentiment > 25 && x.sentiment < 75 });
    body += "<br>"
    body += "<b>Positive Tweets</b>";
    body += "<br>"
    for (var t in positiveTweets) {
        body += positiveTweets[t].user;
        body += " - ";
        body += positiveTweets[t].text;
        body += "<br>";
    }
    body += "<b>Negative Tweets</b>";
    body += "<br>"
    for (var t in negativeTweets) {
        body += negativeTweets[t].user;
        body += " - ";
        body += negativeTweets[t].text;
        body += "<br>";
    }
    body += "<b>Neutral Tweets</b>";
    body += "<br>"
    for (var t in neutralTweets) {
        body += neutralTweets[t].user;
        body += " - ";
        body += neutralTweets[t].text;
        body += "<br>";
    }
    body += " </body> </html>";
    console.log(body);
});



