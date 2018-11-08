import math

def uniquelines(lineslist):
    unique = {}
    result = []
    for item in lineslist:
        if item.strip() in unique: continue
        unique[item.strip()] = 1
        result.append(item)
    return result

sample_file = uniquelines(open('workfile.txt').read().splitlines())

number_of_points = len(sample_file)
print('Number of points: ' + str(number_of_points))

if number_of_points <= 3:
    print('TAK')
    exit()

#First three points needed to find plain equasion
p = sample_file[0].split()
p = [float(i) for i in p]
q = sample_file[1].split()
q = [float(i) for i in q]
r = sample_file[2].split()
r = [float(i) for i in r]

#print('p:' + str(p))
#print('q:' + str(q))
#print('r:' + str(r))

#Finding line equasion in case all the points are lined up
#http://tutorial.math.lamar.edu/Classes/CalcIII/EqnsOfLines.aspx
V1 = [p[0]-q[0], p[1]-q[1], p[2]-q[2]]
print('V1: ' + str(V1))
print('p: ' + str(p))

flag = []
for i in range(0, 3):
    if V1[i] != 0:
        flag.append(1)
    else:
        flag.append(0)       

print(flag)

cannot_cach_all_flies = 0 
not_in_line = 0 

for i in range(0, number_of_points):        
    fly = sample_file[i].split() 
    fly = [float(i) for i in fly]
    print('fly: ' + str(i) + ' ' + str(fly))
    sym_form = []
    if flag[0] == 1:
        sym_form.append((fly[0]-p[0])/V1[0])
    else:
        sym_form.append(fly[0])

    if flag[1] == 1:
        sym_form.append((fly[1]-p[1])/V1[1])
    else:
        sym_form.append(fly[1])

    if flag [2] == 1:
        sym_form.append((fly[2]-p[2])/V1[2])
    else:
        sym_form.append(fly[2])    

    print('sym: ' + str(i) + ' ' + str(sym_form) + '\n')

    tmp = []  
    for j in range (0, 3):              #iteracja po kolumnach  
        if flag[j] == 0 and i != 0:                
            if sym_form[j] != p[j]:     
                not_in_line += 1
        elif flag[j] == 1 and i != 0:   
            tmp.append(sym_form[j])

        if flag[j] == 0 and i == 0:                
            if sym_form[j] != q[j]:     
                not_in_line += 1
        elif flag[j] == 1 and i == 0:   
            tmp.append(sym_form[j])

    #print('LEN TMP: ' + str(len(tmp)))
    #print(tmp)
    if len(tmp) == 2: 
        if tmp[0] != tmp[1]:
            not_in_line += 1
    elif len(tmp) == 3:
        if tmp[0] != tmp[1] or tmp[0] != tmp[2]:
            not_in_line += 1

    if not_in_line > 1 and i > 2:
        r = fly

    if not_in_line > 1: 
        break

#Finding plain equasion
#http://tutorial.math.lamar.edu/Classes/CalcIII/EqnsOfPlanes.aspx
#a(x-x0) + b(y-y0) + c(z-z0) = d
RQ = [q[0]-r[0], q[1]-r[1], q[2]-r[2]]
RP = [p[0]-r[0], p[1]-r[1], p[2]-r[2]]

a = RQ[1]*RP[2] - RP[1]*RQ[2]
b = RQ[2]*RP[0] - RP[2]*RQ[0]
c = RQ[0]*RP[1] - RP[0]*RQ[1]
d = a*p[0] + b*p[1] + c*p[2] 

#DEBUG
#print(str(p[0]) + " " + str(p[1]) + " " + str(p[2]))
#print(str(q[0]) + " " + str(q[1]) + " " + str(q[2]))
#print(str(r[0]) + " " + str(r[1]) + " " + str(r[2]))
#print("\n")
#print(str(RQ[0]) + " " + str(RQ[1]) + " " + str(RQ[2]))
#print(str(RP[0]) + " " + str(RP[1]) + " " + str(RP[2]))
#print("\n")
#print('a= ' + str(a))
print(r)
#print('RQ: ' + str(RQ))
#print('RP: ' + str(RP))
print(f'{a:.9f}')
print('b= ' + str(b))
print('c= ' + str(c))
print('d= ' + str(d))
#DEBUG


print('not in line: ' + str(not_in_line))
if not_in_line > 1: 
    print('halo')
    for i in range(0, number_of_points):
        fly = sample_file[i].split() 
        fly = [float(i) for i in fly]

        #print('1: ' + str(fly[0]))
        #print('2: ' + str(fly[1]))
        #print('3: ' + str(fly[2]))
        #print('d: ' + str(d))
        #print(str(d-(a*fly[0] + b*fly[1] + c*fly[2])))
     
        if a*fly[0] + b*fly[1] + c*fly[2] != d and math.fabs(d - (a*fly[0] + b*fly[1] + c*fly[2])) > 0.0000001:
            cannot_cach_all_flies += 1
            #print('cannot_cach_all_flies: ' + str(cannot_cach_all_flies))

if not_in_line == 0: 
    print('leza w jednej lini')

if cannot_cach_all_flies == 0:
    print('TAK')
else:
    print('NIE')

