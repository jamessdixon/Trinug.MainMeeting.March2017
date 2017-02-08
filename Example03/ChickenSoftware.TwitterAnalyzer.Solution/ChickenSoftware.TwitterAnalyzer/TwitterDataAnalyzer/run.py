#https://text-analytics-demo.azurewebsites.net/
#https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics.V2.0/operations/56f30ceeeda5650db055a3c9

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
    return


#input = open(os.environ['inTable']).read()
#records = [json.loads(line) for line in input]
#get the last 10 max
#for each record, get its sentement



