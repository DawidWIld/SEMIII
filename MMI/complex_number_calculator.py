import math

class complex:
    def __init__(self, real, imag):
        self.real = real
        self.imag = imag
        
    def __str__(self):
        return str(self.real) + "+" + str(self.imag) +"i"
    
    #phase = phi - complex number argument
    def phase(self): 
        return round(math.degrees(math.atan2(self.imag, self.real)), 1)
        #return round(math.degrees(math.acos(self.real/math.sqrt(self.real**2 + self.imag**2))), 1)    #cos(phi)=a/sqrt(a**2+b**2)
    
    def length(self):
         return round(math.sqrt(self.real**2 + self.imag**2), 3)
    
    #z=|z|(cos(phi)+i*sin(phi))
    def tryg(self):
        return "|" + str(self.length()) + "|(cos(" + str(self.phase()) + ")+i*sin(" + str(self.phase()) + ")"
    
    #z=|z|e^{i*phi} 
    def exp(self):
        return "|" + str(self.length()) + "|e^(i*" + str(self.phase()) + ")"
        
    def __add__(self, b):
        return complex(self.real + b.real, self.imag + b.imag)
    
    def __sub__(self, b):
        return complex(self.real - b.real, self.imag - b.imag)
    
    def __mul__(self, b):
        return complex((self.real * b.real) - (self.imag * b.imag), (self.imag * b.real) + (self.real * b.imag))
    
    def __truediv__(self, b):
        return complex(
            ((self.real * b.real) + (a.imag * b.imag)) / ((b.real * b.real) + (b.imag * b.imag)),
            ((self.imag * b.real) - (a.real * b.imag)) / ((b.real * b.real) + (b.imag * b.imag))
            ) 
          
try:
    a_real = float(input("***A*** \nEnter real part of A: "))
    a_imag = float(input("Enter imaginary part of A: "))
    b_real = float(input("***B*** \nEnter real part of B: "))
    b_imag = float(input("Enter imaginary part of B: "))
except ValueError:
    print("Sad frog is sad because you cannot provide proper input :(")
    raise SystemExit

      
a = complex(a_real, a_imag)
b = complex(b_real, b_imag)

if (a+b).length() != 0.0:
    print("\na+b= " + str(a+b) + "\n     " + (a+b).tryg() + "\n     " + (a+b).exp())
else:
    print("a+b= " + str(a*b) + "\n     0"+ "\n     0")

if (a-b).length() != 0.0:
    print("a-b= " + str(a-b) + "\n     " + (a-b).tryg() + "\n     " + (a-b).exp())
else:
    print("a-b= " + str(a*b) + "\n     0"+ "\n     0")

if (a*b).length() != 0.0:
    print("a*b= " + str(a*b) + "\n     " + (a*b).tryg() + "\n     " + (a*b).exp())
else:
    print("a*b= " + str(a*b) + "\n     0"+ "\n     0")

if b.length() != 0.0:
    print("a/b= " + str(a/b) + "\n     " + (a/b).tryg() + "\n     " + (a/b).exp())
else:
    print("a/b DOES NOT EXIST")
