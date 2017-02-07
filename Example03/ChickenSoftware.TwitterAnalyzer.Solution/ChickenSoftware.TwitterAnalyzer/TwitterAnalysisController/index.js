﻿module.exports = function (context, sentimentTable) {
    var lastRecord = sentimentTable[sentimentTable.length - 1]
    var positiveTweets = sentimentTable.filter(function (x) { return x.id == lastRecord.id && x.sentiment == 'positive' });
    var negativeTweets = sentimentTable.filter(function (x) { return x.id == lastRecord.id && x.sentiment == 'negative' });
    var neutralTweets = sentimentTable.filter(function (x) { return x.id == lastRecord.id && x.sentiment == 'neutral' });

    var body = "<!DOCTYPE html> <html> <head> </head> <body> ";
    body += "<b>Overall sentiment score = " + lastRecord.score + "</b>";
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

    context.res = {
        body: body,
        headers: {
            'Content-Type': 'text/html; charset=utf-8'
        }
    };

    context.done();
};