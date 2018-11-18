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
        x = "\033[92m" + "OK" + "\033[0m"
    else:
        x = "\033[91m" + "BAD" + "\033[0m"

    print(name[0] + ": " + str(x))

print("failed: " + str(n-p))
print("passed: " + str(p))
    