﻿import os
import json

input = open(os.environ['input']).read()
tweets = json.loads(input)
#message = "Python script processed queue message '{0}'".format(tweets[''])
print(message)