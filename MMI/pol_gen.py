import random 
import json

m = random.randint(1, 2000)

for i in range(100):
    array = [random.randint(-100, 100) for i in range(m)]
    open('test_' + str(i) , 'w').write(json.dumps(array))