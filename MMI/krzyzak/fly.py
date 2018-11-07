import math

sample_file = open('workfile.txt').read().splitlines()

number_of_points = int(sample_file[0])

p = sample_file[2].split()
p = [float(i) for i in p]
q = sample_file[3].split()
q = [float(i) for i in q]
r = sample_file[4].split()
r = [float(i) for i in r]

PQ = [q[0]-p[0], q[1]-p[1], q[2]-p[2]]
PR = [r[0]-p[0], r[1]-p[1], r[2]-p[2]]

#http://tutorial.math.lamar.edu/Classes/CalcIII/EqnsOfPlanes.aspx
#a(x-x0) + b(y-y0) + c(z-z0) = d
a = PQ[1]*PR[2] - PR[1]*PQ[2]
b = PQ[2]*PR[0] - PR[2]*PQ[0]
c = PQ[0]*PR[1] - PR[0]*PQ[1]
d = a*p[0] + b*p[1] + c*p[2]        

#DEBUG
#print(str(p[0]) + " " + str(p[1]) + " " + str(p[2]))
#print(str(q[0]) + " " + str(q[1]) + " " + str(q[2]))
#print(str(r[0]) + " " + str(r[1]) + " " + str(r[2]))
#print("\n")
#print(str(PQ[0]) + " " + str(PQ[1]) + " " + str(PQ[2]))
#print(str(PR[0]) + " " + str(PR[1]) + " " + str(PR[2]))
#print("\n")
#print('a= ' + str(a))
#print(f'{a:.9f}')
#print('b= ' + str(b))
#print('c= ' + str(c))
#print('d= ' + str(d))
#DEBUG

cannot_cach_all_flies = 0

for i in range(2, number_of_points+2):
    fly = sample_file[i].split() 
    fly = [float(i) for i in fly]

    if a*fly[0] + b*fly[1] + c*fly[2] != d and math.fabs(d-a*fly[0] + b*fly[1] + c*fly[2]) < 0.000000001:
        cannot_cach_all_flies += 1

if cannot_cach_all_flies == 0:
    print('TAK')
else:
    print('NIE')