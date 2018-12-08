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

def fft(x,neg=1):
    N = len(x)
    if N <= 1:
        return x
    even = fft(x[0::2])
    odd = fft(x[1::2])
    T = [cmath.exp(neg*-2j*cmath.pi*k/N)*odd[k] for k in range(N//2)]
    return [even[k] + T[k] for k in range(N//2)] + [even[k] - T[k] for k in range(N//2)]

def invfft(x):
    return [1/len(x) * i for i in fft(x, -1)]

def fftmul(a, b):
    fa = fft(a)
    fb = fft(b)
    fc = [fa[i] * fb[i] for i in range(len(a))]
    return [c.real for c in invfft(fc)]

print(slowmul(a,b))
print(fftmul(a,b))

print(calc(slowmul(a,b), 1j))
print(calc(fftmul(a,b), 1j))

#print(invfft(fft(a)))
