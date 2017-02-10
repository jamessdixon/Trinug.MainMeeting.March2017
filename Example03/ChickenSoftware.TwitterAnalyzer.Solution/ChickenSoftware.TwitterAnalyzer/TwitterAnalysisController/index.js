module.exports = function (context, inBlob) {
    var body = "<!DOCTYPE html> <html> <head> </head> <body> ";
    
    if (inBlob != null) {
        var analysis = context.bindings.inBlob;
        body += "<b>Overall sentiment score = " + analysis.totalSentiment + "</b>";
        var positiveTweets = analysis.data.filter(function (x) { return x.sentiment >= .75 });
        var negativeTweets = analysis.data.filter(function (x) { return x.sentiment <= .25 });
        var neutralTweets = analysis.data.filter(function (x) { return x.sentiment > .25 && x.sentiment < .75 });
        body += "<br>"
        body += "<b>Positive Tweets</b>";
        body += "<br>"
        for (var t in positiveTweets) {
            body += positiveTweets[t].user_name;
            body += " - ";
            body += positiveTweets[t].text;
            body += " (";
            body += positiveTweets[t].sentiment;
            body += " )";
            body += "<br>";
        }
        body += "<b>Negative Tweets</b>";
        body += "<br>"
        for (var t in negativeTweets) {
            body += negativeTweets[t].user_name;
            body += " - ";
            body += negativeTweets[t].text;
            body += " (";
            body += negativeTweets[t].sentiment;
            body += " )";
            body += "<br>";
        }
        body += "<b>Neutral Tweets</b>";
        body += "<br>"
        for (var t in neutralTweets) {
            body += neutralTweets[t].user_name;
            body += " - ";
            body += neutralTweets[t].text;
            body += " (";
            body += neutralTweets[t].sentiment;
            body += " )";
            body += "<br>";
        }
    }
    else {
        body += "No Analysis Yet...";
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