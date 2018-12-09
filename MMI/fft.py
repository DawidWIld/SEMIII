import cmath

a = [2,1,3,1,3,0,0,0,0,0,0,0,0,0,0,0]
b = [1,1,1,8,0,0,0,0,0,0,0,0,0,0,0,0]

def calc(w, cmplx):
    result = 0
    for x in range(len(w)):
        result += cmplx**x * w[x]
    return result

def slowmul(a, b):
    c = [0 for x in range(len(a) + len(b))]
    for x in range(len(a)):
        for y in range(len(b)):
            c[x+y] += a[x] * b[y]
    return c

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

def fftmul(a, b):
    fa = fft(a)
    fb = fft(b)
    fc = [fa[i] * fb[i] for i in range(len(a))]
    tmp = [c.real for c in ifft(fc)]
    return tmp

print(slowmul(a,b))
print(fftmul(a,b))

print(calc(slowmul(a,b), 1j + 3))
print(calc(fftmul(a,b), 1j + 3))
