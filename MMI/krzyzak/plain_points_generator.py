import random

number_of_points = random.randint(4, 100)
#number_of_points = 3
file = open('workfile.txt', 'a+')
file.write(str(number_of_points))

#plain equation:
#  ax+by+cz=d

#generating random plain
abc = [random.randint(-10, 10), random.randint(-10, 10), random.randint(-10, 10)]
d = random.randint(-20, 20)
file.write("\n" + str(abc[0]) + "x" + str(abc[1]) + "y" + str(abc[2]) + "z=" + str(d))

#checking if a=/=b=/=c=/=0
while abc[0] == 0 and abc[1] == 0 and abc[2] == 0: 
    abc[random.randint(0, 2)] = random.randint(-10, 10)   

#generating random points that fits the plain
for i in range (0, number_of_points):
    point = [random.randint(-100000, 100000), random.randint(-100000, 100000)]          #x an y generated randomly
    point.append((abc[0]*point[0] + abc[1]*point[1] - d) / -abc[2])                     #z must fit the plain equasion 
    file.write("\n" + str(point[0]) + " " + str(point[1]) + " " + str(point[2])) 