module.exports = function (context, sentimentTable) {
    var body = "<html> <head> </head> <body>Hello World</body> </html>";
    context.res.body = body;
    context.res.contentType = "html"
    context.done();
};

/*
id	DateTime	Sentiment	Sentiment	User	Text
1	2/6/2017	8.2	        Good	Ian	        #TRINUG Rulz
2	2/6/2017	8.2	Good	Jamie	            #TRINUG is the bomb
3	2/6/2017	8.2	Good	Greg	
4	2/6/2017	8.2	Good	Justin	
5	2/6/2017	8.2	Bad	    Jeremy	            #TRINUG blows
6	2/6/2017	8.2	Bad	    Jim	


*/