import os
import subprocess

n = 0
p = 0

for file in os.listdir("./krz/in/"):
    n += 1
    name = os.path.splitext(file)
    _input = "./krz/in/" + name[0] + ".in"
    _output = "./krz/out/" + name[0] + ".out"
    result = subprocess.run(["python", "fly.py", _input], stdout=subprocess.PIPE)
    result_from_file = open(_output, "r").read()
    #print(result.stdout)
    #print(result_from_file)

    x = result.stdout.decode("ASCII").strip() == result_from_file.strip()
    if x:
        p += 1

    print(name[0] + ": " + str(x))

print("n: " + str(n-p))
print("p: " + str(p))
    