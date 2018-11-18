import math
import sys

def uniquelines(lineslist):
    unique = {}
    result = []
    for item in lineslist:
        if item.strip() in unique: continue
        unique[item.strip()] = 1
        result.append(item)
    return result

class Vector():

    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z

    def cross_product(a, b):
        return Vector(a.y*b.z-a.z*b.y, a.z*b.x-a.x*b.z, a.x*b.y-a.y*b.x)

    def __str__(self):
        return str([self.x, self.y, self.z])


sample_file = uniquelines(open(sys.argv[1]).read().splitlines())
sample_file.pop(0)
points = [[int(i) for i in line.split()] for line in sample_file]

number_of_points = len(points)

if number_of_points <= 3:
    print('TAK')
    exit()

#First three points needed to find plain equasion
p = points[0]
q = points[1]
r = points[2]

#Finding line equasion in case all the points are lined up
#http://tutorial.math.lamar.edu/Classes/CalcIII/EqnsOfLines.aspx
V1 = Vector(p[0]-q[0], p[1]-q[1], p[2]-q[2])
not_in_line = 0 

for point in points:
    V2 = Vector(p[0]-point[0], p[1]-point[1], p[2]-point[2])
    cross = Vector.cross_product(V1, V2)
    if cross.x != 0 or cross.y != 0 or cross.z != 0:
        not_in_line += 1
        r = point
        #print(str(points.index(point) + 1) + " " + str(cross))

#Finding plain equasion
#http://tutorial.math.lamar.edu/Classes/CalcIII/EqnsOfPlanes.aspx
#a(x-x0) + b(y-y0) + c(z-z0) = d
RQ = [q[0]-r[0], q[1]-r[1], q[2]-r[2]]
RP = [p[0]-r[0], p[1]-r[1], p[2]-r[2]]

a = RQ[1]*RP[2] - RP[1]*RQ[2]
b = RQ[2]*RP[0] - RP[2]*RQ[0]
c = RQ[0]*RP[1] - RP[0]*RQ[1]
d = a*p[0] + b*p[1] + c*p[2] 

cannot_cach_all_flies = 0

if not_in_line > 1: 
    for i in range(0, number_of_points):
        fly = points[i]
     
        if a*fly[0] + b*fly[1] + c*fly[2] != d:
            cannot_cach_all_flies += 1

if cannot_cach_all_flies == 0:
    print('TAK')
else:
    print('NIE')
