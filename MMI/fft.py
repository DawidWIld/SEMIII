import cmath
import json
import sys
import time

def padding(a, b):
    a += [0 for i in range(len(b) - 1)]
    m = 1 << (len(a)-1).bit_length() #binary left shift (makes sure that m is a power of 2)
    a += [0 for i in range(m - len(a))]
    b += [0 for i in range(len(a) - len(b))]

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

for i in range(50):
    a = json.loads(open("fft_tests/test_"+str(i*2), "r").read());
    b = json.loads(open("fft_tests/test_"+str(i*2+1), "r").read());
    padding(a, b)

    start_time = time.perf_counter_ns()
    muls = slowmul(a,b)
    time_s =  time.perf_counter_ns() - start_time

    start_time = time.perf_counter_ns()
    mulf = fftmul(a,b)
    time_f = time.perf_counter_ns() - start_time

    result_slow = calc(muls, 1/cmath.sqrt(2) + 1j/cmath.sqrt(2));
    result_fast = calc(muls, 1/cmath.sqrt(2) + 1j/cmath.sqrt(2));

    print("TEST " + '{:>2}'.format(i+1) + ": ", end='');
    if abs(result_slow - result_fast) < 0.01: # probably works fine but not rly sure 
        print("\033[92m" + "OK" + "\033[0m", end="")
    else:
        print("\033[91m" + "BAD" + "\033[0m", end="")
    print("    SLOW: " + '{:.2f}'.format(time_s/1000000) + "ms\t FFT: " + '{:.2f}'.format(time_f/1000000) + "ms " + '({:.2f}'.format(time_f/time_s * 100) + "%)");

    sys.stdout.flush()
