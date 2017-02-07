import os
import json

input = open(os.environ['inTable']).read()
records = [json.loads(line) for line in input]

print(input)
