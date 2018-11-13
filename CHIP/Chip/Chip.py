import io
import re

file = io.open('workfile.txt','r', encoding='cp850').read().splitlines()

class ChipMap():
	def __init__(self, name, desc, address, x, y):
		self.name = name
		self.desc = desc
		self.address = address
		self.x = x
		self.y = y

	def __str__(self):
		return f"{self.name} {self.desc} {self.address}"

which_line = 0
maps = []

for x in range(len(file)):
    which_line += 1
    line = file[x]
    #data = line.split(',')
    #data = csv.reader(line)
	# ([^,]+),([^,]+),([^,]+), {([^}]*)\}, ([0-9]+),([^,]+),([^,]+)
    if which_line == 1:
        match = re.match(r'([^,]+), ([^,]+), ([^,]+), {([^}]*)\}, ([0-9a-fA-F]+), ([^,]+), ([^,]+)', line)
        if match == None:
            continue
        #print(match)

        m = ChipMap(match.group(3), match.group(4), match.group(6), 0, 0)
        maps.append(m)
    if which_line == 8:
        which_line = 0


for m in maps:
	print(str(m))




'''
22, /SPZ, AHEKA, {Aussetzerhäufigkeit zum Erreichen einer Kraftstoffabschaltung}, 1, $813248, $813248
/SPW, 22, 2, 0, 65535
/SPX, 0, 0, 0, 0, 0
/SPY, 0, 0, 0, 0, 0
/FKX, 0, 0, 0
/FKY, 0, 0, 0
/ABL, 0;

'''

