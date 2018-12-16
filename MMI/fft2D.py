import cmath
import math
import matplotlib.pyplot as plot
import matplotlib.image as image

def load_image(dir):
    return image.imread(dir)

def fft(x):
    n = len(x)
    if n <= 1:
        return x
    even = fft(x[0::2])
    odd = fft(x[1::2])
    T = [cmath.exp(-2j*cmath.pi*k/n)*odd[k] for k in range(n//2)]
    return [even[k] + T[k] for k in range(n//2)] + [even[k] - T[k] for k in range(n//2)]

def ifft(x):
    return [(1/len(x) * i).conjugate() for i in fft([i.conjugate() for i in x])]

def fft2d(data):
    result = [[0 for y in range(len(data[0]))] for x in range(len(data))]
    for i in range(len(data)):
        row = fft(data[i])
        result[i] = row

    for j in range(len(data[0])):
        column = fft([result[i][j] for i in range(len(data))])
        for x in range(len(data)):
            result[x][j] = column[x]
    
    return result

def ifft2d(data):
    result = [[0 for y in range(len(data[0]))] for x in range(len(data))]

    for j in range(len(data[0])):
        column = ifft([data[i][j] for i in range(len(data))])
        for x in range(len(data)):
            result[x][j] = column[x]


    for i in range(len(data)):
        row = ifft(result[i])
        for x in range(len(data[0])):
            result[i][x] = row[x]
    
    return result

def norm(data):
    max_value = data[0][0]
    min_value = data[0][0]
    for x in data:
        for y in x:
            if y > max_value: max_value = y
            if y < min_value: min_value = y

    for x in range(len(data)):
        for y in range(len(data[0])):
            data[x][y] = (data[x][y] - min_value) / (max_value - min_value)

img = load_image('test2.png')
canals = len(img[0][0])

img_fft = [fft2d(img[:,:,x]) for x in range(canals)]

#tu trzeba zrobic magie i beda effekty graficzne hehe

R = 36
r = 0
A = math.pi * 2
a = 0

center = len(img) // 2

for x in range(len(img)):
    for y in range(len(img[0])):
        #center 32x32
        if(math.atan2(x, y) < a) or (math.atan2(x, y) > A) or ((x - center)**2 + (y - center)**2 > R**2) or ((x - center)**2 + (y - center)**2 < r**2):
            #for i in range(canals):
            img_fft[0][x][y] = 0
            img_fft[1][x][y] = 0
            img_fft[2][x][y] = 0
      

img_fft = [ifft2d(img_fft[x]) for x in range(canals)]

for x in range(len(img)):
    for y in range(len(img[0])):
        img[x][y] = [img_fft[i][x][y].real for i in range(canals)]

imgplot = plot.imshow(img)
plot.show();

#print(fft2d(data))