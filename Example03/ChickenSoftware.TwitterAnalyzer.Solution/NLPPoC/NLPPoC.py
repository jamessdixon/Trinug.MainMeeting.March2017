
import httplib, urllib, base64

headers = {
    'Content-Type': 'application/json',
    'Ocp-Apim-Subscription-Key': '{subscription key}',
}

params = urllib.urlencode({
})

try:
    conn = httplib.HTTPSConnection('westus.api.cognitive.microsoft.com')
    conn.request("POST", "/text/analytics/v2.0/sentiment?%s" % params, "{body}", headers)
    response = conn.getresponse()
    data = response.read()
    print(data)
    conn.close()
except Exception as e:
    print("[Errno {0}] {1}".format(e.errno, e.strerror))

#import json

#path = 'TweetLite.json'
#input = open(path).read()
#obj = json.loads(input)
#print(obj)

#records = [json.loads(line) for line in open(path,'r')]
#text = [rec['text'] for rec in records if 'text' in rec]

#https://text-analytics-demo.azurewebsites.net/
#https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics.V2.0/operations/56f30ceeeda5650db055a3c9

#print(records)

    