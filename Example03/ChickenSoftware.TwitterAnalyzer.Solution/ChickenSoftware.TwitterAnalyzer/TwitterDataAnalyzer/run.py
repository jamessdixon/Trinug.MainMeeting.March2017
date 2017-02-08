
import httplib, urllib, base64, json

headers = {
    'Content-Type': 'application/json',
    'Ocp-Apim-Subscription-Key': '15ac8ef72e3c4458a90cbdb024c68466'
}

params = urllib.urlencode({})

def getSentimentScore(text):
    try:
        body = '{"documents": [{"language": "en","id": "1","text": "' + text + '"}]}'
        conn = httplib.HTTPSConnection('westus.api.cognitive.microsoft.com')
        conn.request("POST", "/text/analytics/v2.0/sentiment?%s" % params, body, headers)
        response = conn.getresponse()
        json_data = response.read()
        conn.close()
        data =  json.loads(json_data)
        return float(data['documents'][0]['score'])
    except Exception as e:
        return 50.0

def createSentimentItem(d):
    return {
    'user_name':d['user_name'],
    'text': d['text'],
    'retweet_count': d['retweet_count'],
    'sentiment':getSentimentScore(d['text'])}

def calculateTotalSentimentScore(l):
    sumProduct = sum(map(lambda d: float(d['sentiment']) * float(d['retweet_count']),l))
    sumWeight =  sum(map(lambda d: float(d['retweet_count']),l))  
    return sumProduct/sumWeight

json_data = open(os.environ['inTable']).read()
data = json.loads(json_data)
sortedData = sorted(data, key=lambda tl: tl['created_at'],reverse=True)
topData = sortedData[:10]
sentimentItems = map(createSentimentItem, topData)
totalSentiment = calculateTotalSentimentScore(sentimentItems)

final = {'runDateTime':datetime.datetime.utcnow().isoformat(), 'totalSentiment':totalSentiment,'data':sentimentItems}
output = json.dumps(final)

outputTable = open(os.environ['outTable']).read()




