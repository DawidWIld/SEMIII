import math

#SPRAWDZIC WYPUKLOSC WIELKATA

def perimeter(x, y):
    peri = 0
    for i in range(len(x) - 1):
        dx = x[i + 1] - x[i]
        dy = y[i + 1] - y[i]
        peri += math.sqrt(dx**2 + dy**2)
    return peri

# |1/2 * (y2+y1)*(x2-x1) + ... |
def area(x, y):
    ar = 0
    for i in range(len(x) - 1):
        ar += (y[i + 1]+y[i])*(x[i + 1] - x[i])
    
    return abs(ar / 2)

def translate(x, y, px, py):
    for i in range(len(x)):
        x[i] += px
        y[i] += py

def scale_around(x, y, scale, px, py):
    for i in range(len(x)):
        x[i] = (1 - scale) * px + scale * x[i]
        y[i] = (1 - scale) * py + scale * y[i]

# x' = a + (x-a)cos(alpha) - (y-b)sin(alpha)
# y' = b + (x-a)sin(alpha) + (y-b)cos(alpha)
# a,b - point coords
def rotate_around(x, y, angle, px, py):
    for i in range(len(x)):
        x1 = x[i]
        x[i] = px + (x[i] - px) * math.cos(angle) - (y[i] - py) * math.sin(angle) 
        y[i] = py + (x1 - px) * math.sin(angle) + (y[i] - py) * math.cos(angle)

def draw(x, y):
    pass

#count = int(input("Number of vertices: "))

x = [0,1,1,0]
y = [0,0,1,1]

#for i in range(count):
#    x.append(float(input("\nx coordinate: ")))
#    y.append(float(input("y coordinate: ")))
    
x.append(x[0])
y.append(y[0])

print("\nPerimeter: {}\n".format(perimeter(x, y)))
print("Area: {}\n".format(area(x, y)))

draw(x, y)

translate(x, y, 2, 1)
scale_around(x, y, 2, 0, 0)
rotate_around(x, y, 1/8*math.pi, 1, 1)

for i in range(len(x)):    
    print(str(round(x[i], 5)) + " " + str(round(y[i],2)))

print("\nPerimeter after scaling: {}\n".format(perimeter(x, y)))
print("Area after scaling: {}\n".format(area(x, y)))


draw(x, y)